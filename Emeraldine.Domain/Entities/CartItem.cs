using System.Text.Json.Serialization;

namespace Emeraldine.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }
    [JsonIgnore]
    public Cart Cart { get; set; } = null!;

    public Guid PlantVariantId { get; set; }
    public PlantVariant PlantVariant { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPriceExVat { get; set; }

    public double WeightKg { get; set; }
}