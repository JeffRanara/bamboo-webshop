using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class Harvest
{
    public Guid Id { get; set; }

    // Optional link to a specific culm
    public Guid? CulmId { get; set; }
    public Culm? Culm { get; set; }

    // Optional link to plant if harvest is not culm-specific
    public Guid? PlantUnitId { get; set; }
    public PlantUnit? PlantUnit { get; set; }

    // Harvest details
    public DateTime HarvestDate { get; set; }
    public HarvestType HarvestType { get; set; }

    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;

    public string? Purpose { get; set; }
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}