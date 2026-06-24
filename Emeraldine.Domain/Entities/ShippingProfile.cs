using System.Text.Json.Serialization;

using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class ShippingProfile
{
    public Guid Id { get; set; }

    public Guid PlantVariantId { get; set; }
    [JsonIgnore]
    public PlantVariant PlantVariant { get; set; } = null!;

    public double WeightKg { get; set; }

    public int PackageLengthCm { get; set; }
    public int PackageWidthCm { get; set; }
    public int PackageHeightCm { get; set; }

    public ShippingCategory ShippingCategory { get; set; }

    public bool CanShip { get; set; } = true;
    public bool CanPickup { get; set; } = true;

    public string? ShippingNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}