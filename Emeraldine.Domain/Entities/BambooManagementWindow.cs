using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

public class BambooManagementWindow
{
    public Guid Id { get; set; }

    public Guid BambooSpeciesId { get; set; }
    public BambooSpecies BambooSpecies { get; set; } = null!;

    public BambooManagementTaskType TaskType { get; set; }

    public int StartMonth { get; set; }
    public int? StartDay { get; set; }

    public int EndMonth { get; set; }
    public int? EndDay { get; set; }

    public int? RecommendedCulmAgeYearsLow { get; set; }
    public int? RecommendedCulmAgeYearsHigh { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}