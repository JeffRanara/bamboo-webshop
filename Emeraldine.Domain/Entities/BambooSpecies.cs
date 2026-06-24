

using Emeraldine.Domain.Enums;

namespace Emeraldine.Domain.Entities;

using System.ComponentModel.DataAnnotations;



public class BambooSpecies
{
    public Guid Id { get; set; }

    // Botanical Identity
    [Required] 
    public string Genus { get; set; } = string.Empty;
    [Required] 
    public string Species { get; set; } = string.Empty;
    public string? Cultivar { get; set; }
    public string? CommonName { get; set; }

    // Growth Characteristics
    public GrowthHabit Habit { get; set; }

    public double HeightLow { get; set; }
    public double HeightHigh { get; set; }

    public double DiameterLow { get; set; }
    public double DiameterHigh { get; set; }

    public double HardinessLow { get; set; }

    public int TypicalShootingMonth { get; set; }

    // Domain Notes
    public string? Notes { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public BambooTechnicalProfile? TechnicalProfile { get; set; }

    public List<PlantVariant> PlantVariants { get; set; } = new();
}