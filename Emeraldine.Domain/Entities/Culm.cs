using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class Culm
{
    public Guid Id { get; set; }

    // Link to plant
    public Guid PlantUnitId { get; set; }
    public PlantUnit PlantUnit { get; set; } = null!;

    // Identification
    public string? PhysicalTagCode { get; set; }

    // Core Domain Concept
    public int EmergenceYear { get; set; }

    // Physical properties (optional tracking)
    public double? HeightMeters { get; set; }
    public double? DiameterCm { get; set; }

    // Status
    public CulmStatus Status { get; set; } = CulmStatus.Growing;

    // Lifecycle dates
    public DateTime? HarvestedDate { get; set; }
    public DateTime? RemovedDate { get; set; }

    // Notes
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}