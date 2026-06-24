using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class PlantUnit
{
    public Guid Id { get; set; }

    // Link to acquisition batch
    public Guid InventoryBatchId { get; set; }
    public InventoryBatch InventoryBatch { get; set; } = null!;

    // Identification
    public string? PlantCode { get; set; }

    // Location / status
    public string? CurrentLocationDescription { get; set; }
    public PlantUnitStatus Status { get; set; } = PlantUnitStatus.Active;

    // Dates
    public DateTime? PlantedDate { get; set; }
    public DateTime? RemovedDate { get; set; }

    // Notes
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}