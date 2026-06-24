using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public string? CustomerEmail { get; set; }

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.PendingPayment;

    public decimal SubtotalExVat { get; set; }
    public decimal VatAmount { get; set; }
    public decimal ShippingAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalIncVat { get; set; }

    public string? ShippingMethod { get; set; }
    public string? ShippingAddressJson { get; set; }

    public string? StripeCheckoutSessionId { get; set; }
    public string? StripePaymentIntentId { get; set; }

    public List<OrderItem> Items { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
    public DateTime? RefundedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}