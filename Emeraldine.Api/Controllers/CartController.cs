using Emeraldine.Domain.Entities;
using Emeraldine.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emeraldine.Api.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly BambooEnterpriseDbContext _context;

    public CartController(BambooEnterpriseDbContext context)
    {
        _context = context;
    }

    [HttpGet("{sessionId}")]
    public async Task<ActionResult<Cart>> GetCart(string sessionId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.PlantVariant)
                    .ThenInclude(v => v.ShippingProfile)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);

        if (cart == null)
        {
            cart = new Cart { SessionId = sessionId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(AddToCartRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SessionId))
            return BadRequest("SessionId is required.");

        if (request.Quantity <= 0)
            return BadRequest("Quantity must be greater than zero.");

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == request.SessionId);

        if (cart == null)
        {
            cart = new Cart { SessionId = request.SessionId };
            _context.Carts.Add(cart);
        }

        var variant = await _context.PlantVariants
            .Include(v => v.ShippingProfile)
            .FirstOrDefaultAsync(v => v.Id == request.PlantVariantId);

        if (variant == null)
            return NotFound("Plant variant not found.");

        if (variant.ShippingProfile == null)
            return BadRequest($"PlantVariant {variant.Id} has no ShippingProfile.");

        var existingItem = cart.Items
            .FirstOrDefault(i => i.PlantVariantId == variant.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                PlantVariantId = variant.Id,
                Quantity = request.Quantity,
                UnitPriceExVat = variant.PriceExVat,
                WeightKg = variant.ShippingProfile.WeightKg
            });
        }

        await _context.SaveChangesAsync();

        return Ok();
    }
    public class ClearCartRequest
    {
        public string SessionId { get; set; } = null!;
    }

    [HttpPost("clear")]
    public async Task<IActionResult> ClearCart([FromBody] ClearCartRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.SessionId))
            return BadRequest("SessionId is required.");

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == request.SessionId);

        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
}

public class AddToCartRequest
{
    public string SessionId { get; set; } = null!;
    public Guid PlantVariantId { get; set; }
    public int Quantity { get; set; }
}