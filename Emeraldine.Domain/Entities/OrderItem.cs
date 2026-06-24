using System.Text.Json.Serialization;

namespace Emeraldine.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    [JsonIgnore]
    public Order Order { get; set; } = null!;

    public Guid PlantVariantId { get; set; }
    public PlantVariant PlantVariant { get; set; } = null!;

    public string ProductName { get; set; } = null!;
    public int PotSizeLiters { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPriceExVat { get; set; }
    public decimal LineTotalExVat { get; set; }

    public double UnitWeightKg { get; set; }
    public double LineWeightKg { get; set; }

    public string? ShippingCategorySnapshot { get; set; }
    public bool RequiresCulmTrimmingForShipping { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}