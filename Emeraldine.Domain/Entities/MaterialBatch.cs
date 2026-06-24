using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class MaterialBatch
{
    public Guid Id { get; set; }

    // Source harvest
    public Guid HarvestId { get; set; }
    public Harvest Harvest { get; set; } = null!;

    // Identification
    public string? BatchCode { get; set; }

    // Material description
    public MaterialType MaterialType { get; set; }

    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;

    // Processing details
    public string? ProcessingMethod { get; set; }
    public DateTime? ProcessedDate { get; set; }

    // Notes
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}