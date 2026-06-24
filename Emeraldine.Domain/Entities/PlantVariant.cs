using System.Text.Json.Serialization;

namespace Emeraldine.Domain.Entities;

public class PlantVariant
{
    public Guid Id { get; set; }

    public Guid BambooSpeciesId { get; set; }
    [JsonIgnore]
    public BambooSpecies BambooSpecies { get; set; } = null!;

    public int PotSizeLiters { get; set; }

    public decimal PriceExVat { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; } = true;

    public int? EstimatedPlantHeightLowCm { get; set; }
    public int? EstimatedPlantHeightHighCm { get; set; }

    public bool RequiresCulmTrimmingForShipping { get; set; }
    public string? VariantNotes { get; set; }

    public ShippingProfile? ShippingProfile { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}