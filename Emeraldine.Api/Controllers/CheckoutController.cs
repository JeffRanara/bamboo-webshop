using Emeraldine.Domain.Entities;
using Emeraldine.Domain.Enums;
using Emeraldine.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emeraldine.Api.Controllers;

using Stripe.Checkout;

[ApiController]
[Route("api/checkout")]
public class CheckoutController : ControllerBase
{
    private readonly BambooEnterpriseDbContext _context;
    private readonly IConfiguration _configuration;

    public CheckoutController(BambooEnterpriseDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("create-order-from-cart")]
    public async Task<ActionResult<Guid>> CreateOrderFromCart([FromBody] CreateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SessionId))
            return BadRequest("SessionId is required.");

        var cart = await _context.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.PlantVariant)
                    .ThenInclude(v => v.BambooSpecies)
            .Include(c => c.Items)
                .ThenInclude(i => i.PlantVariant)
                    .ThenInclude(v => v.ShippingProfile)
            .FirstOrDefaultAsync(c => c.SessionId == request.SessionId);

        if (cart == null || !cart.Items.Any())
            return BadRequest("Cart is empty.");

        // Validate everything once
        foreach (var item in cart.Items)
        {
            if (item.PlantVariant == null)
                return BadRequest($"Cart item {item.Id} has no PlantVariant.");

            if (item.PlantVariant.BambooSpecies == null)
                return BadRequest($"PlantVariant {item.PlantVariantId} has no BambooSpecies.");

            if (item.PlantVariant.ShippingProfile == null)
                return BadRequest($"PlantVariant {item.PlantVariantId} has no ShippingProfile.");
        }

        var subtotalExVat = cart.Items.Sum(i => i.UnitPriceExVat * i.Quantity);

        var vatRate = 0.25m;
        var vatAmount = subtotalExVat * vatRate;

        var shippingAmount = CalculateShipping(cart.Items);

        var totalIncVat = subtotalExVat + vatAmount + shippingAmount;

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = GenerateOrderNumber(),
            CustomerEmail = request.CustomerEmail,

            PaymentStatus = PaymentStatus.PendingPayment,

            SubtotalExVat = subtotalExVat,
            VatAmount = vatAmount,
            ShippingAmount = shippingAmount,
            DiscountAmount = 0,
            TotalIncVat = totalIncVat,

            ShippingMethod = "Standard",
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in cart.Items)
        {
            var variant = item.PlantVariant!;
            var species = variant.BambooSpecies!;
            var shippingProfile = variant.ShippingProfile!;

            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                PlantVariantId = variant.Id,
                ProductName = $"{species.Genus} {species.Species}",
                PotSizeLiters = variant.PotSizeLiters,

                Quantity = item.Quantity,
                UnitPriceExVat = item.UnitPriceExVat,
                LineTotalExVat = item.UnitPriceExVat * item.Quantity,

                UnitWeightKg = shippingProfile.WeightKg,
                LineWeightKg = shippingProfile.WeightKg * item.Quantity,

                ShippingCategorySnapshot = shippingProfile.ShippingCategory.ToString(),
                RequiresCulmTrimmingForShipping = variant.RequiresCulmTrimmingForShipping
            });
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var stripeSecretKey = _configuration["Stripe:SecretKey"];
        var successUrl = _configuration["Stripe:SuccessUrl"];
        var cancelUrl = _configuration["Stripe:CancelUrl"];
        Console.WriteLine($"STRIPE SUCCESS URL = {successUrl}");

        Stripe.StripeConfiguration.ApiKey = stripeSecretKey;

        var lineItems = order.Items.Select(item => new SessionLineItemOptions
        {
            Quantity = item.Quantity,
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "sek",
                UnitAmount = (long)(item.UnitPriceExVat * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.ProductName
                }
            }
        }).ToList();

        lineItems.Add(new SessionLineItemOptions
        {
            Quantity = 1,
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "sek",
                UnitAmount = (long)((order.VatAmount + order.ShippingAmount) * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "VAT and shipping"
                }
            }
        });

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            CustomerEmail = order.CustomerEmail,
            Metadata = new Dictionary<string, string>
    {
        { "orderId", order.Id.ToString() }
    },
            LineItems = lineItems
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return Ok(new
        {
            order.Id,
            order.OrderNumber,
            CheckoutUrl = session.Url,
            SuccessUrlUsed = successUrl,
            CancelUrlUsed = cancelUrl
        });
    }

    [HttpGet("order-from-session/{stripeSessionId}")]
    public async Task<IActionResult> GetOrderFromSession(string stripeSessionId)
    {
        if (string.IsNullOrWhiteSpace(stripeSessionId))
        {
            return BadRequest("Stripe session id is required.");
        }

        Stripe.StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

        var sessionService = new Stripe.Checkout.SessionService();
        var session = await sessionService.GetAsync(stripeSessionId);

        if (session == null)
        {
            return NotFound("Stripe session not found.");
        }

        if (session.Metadata == null || !session.Metadata.ContainsKey("orderId"))
        {
            return BadRequest("Stripe session does not contain an orderId.");
        }

        var orderIdString = session.Metadata["orderId"];

        if (!Guid.TryParse(orderIdString, out var orderId))
        {
            return BadRequest("Invalid orderId in Stripe session metadata.");
        }

        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        return Ok(order);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var webhookSecret = _configuration["Stripe:WebhookSecret"];

        if (string.IsNullOrWhiteSpace(webhookSecret))
            return BadRequest("Stripe webhook secret is not configured.");

        Stripe.Event stripeEvent;

        try
        {
            stripeEvent = Stripe.EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                webhookSecret
            );
        }
        catch (Stripe.StripeException)
        {
            return BadRequest("Invalid Stripe webhook signature.");
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

            var orderIdText = session?.Metadata?["orderId"];

            if (Guid.TryParse(orderIdText, out var orderId))
            {
                var order = await _context.Orders.FindAsync(orderId);

                if (order != null && order.PaymentStatus == PaymentStatus.PendingPayment)
                {
                    order.PaymentStatus = PaymentStatus.Paid;
                    order.PaidAt = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                }
            }
        }

        return Ok();
    }

    [HttpPost("clear")]
    public async Task<IActionResult> ClearCart([FromBody] string sessionId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);

        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
    private decimal CalculateShipping(List<CartItem> items)
    {
        var totalWeight = items.Sum(i => i.WeightKg * i.Quantity);

        var maxCategory = items
            .Select(i =>
            {
                var variant = i.PlantVariant!;
                var shippingProfile = variant.ShippingProfile!;
                return shippingProfile.ShippingCategory;
            })
            .Max();

        if (maxCategory == ShippingCategory.PickupOnly)
            return 0;

        if (maxCategory >= ShippingCategory.FreightOrPickup || totalWeight > 30)
            return 349;

        if (maxCategory == ShippingCategory.BulkyParcel || totalWeight > 15)
            return 249;

        if (maxCategory == ShippingCategory.MediumParcel || totalWeight > 5)
            return 129;

        return 69;
    }

    private string GenerateOrderNumber()
    {
        return $"BE-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
    }
}

public class CreateOrderRequest
{
    public string SessionId { get; set; } = null!;
    public string? CustomerEmail { get; set; }
}