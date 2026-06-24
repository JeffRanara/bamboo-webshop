using System.Text.Json.Serialization;

namespace Emeraldine.Domain.Entities;

public class BambooTechnicalProfile
{
    public Guid Id { get; set; }

    public Guid BambooSpeciesId { get; set; }
    [JsonIgnore]
    public BambooSpecies BambooSpecies { get; set; } = null!;

    public string? SourceDateBaseline { get; set; }
    public string? DataConfidence { get; set; }
    public string? ConfidenceLevel { get; set; }

    public string? NativeRange { get; set; }
    public int? FloweringIntervalYearsLow { get; set; }
    public int? FloweringIntervalYearsHigh { get; set; }

    public string? GrowthHabit { get; set; }

    public double? AdultHeightLowM { get; set; }
    public double? AdultHeightHighM { get; set; }
    public double? CulmDiameterLowCm { get; set; }
    public double? CulmDiameterHighCm { get; set; }
    public double? WallThicknessLowCm { get; set; }
    public double? WallThicknessHighCm { get; set; }
    public double? InternodeLengthLowCm { get; set; }
    public double? InternodeLengthHighCm { get; set; }

    public double? LeafLengthLowCm { get; set; }
    public double? LeafLengthHighCm { get; set; }
    public double? LeafWidthLowCm { get; set; }
    public double? LeafWidthHighCm { get; set; }
    public double? SheathLengthLowCm { get; set; }
    public double? SheathLengthHighCm { get; set; }
    public double? SheathWidthLowCm { get; set; }
    public double? SheathWidthHighCm { get; set; }

    public double? HardinessMinC { get; set; }
    public double? HardinessMaxC { get; set; }
    public int? SwedishZoneLow { get; set; }
    public int? SwedishZoneHigh { get; set; }

    public bool? LightFullSun { get; set; }
    public bool? LightPartialShade { get; set; }
    public bool? LightShade { get; set; }
    public string? LightRequirementText { get; set; }

    public string? SoilType { get; set; }
    public double? SoilPhLow { get; set; }
    public double? SoilPhHigh { get; set; }
    public string? MoisturePreference { get; set; }

    public string? DroughtTolerance { get; set; }
    public string? WaterloggingTolerance { get; set; }
    public string? WindTolerance { get; set; }
    public string? SnowLoadTolerance { get; set; }
    public string? ColdWindSensitivity { get; set; }
    public string? LateFrostSensitivity { get; set; }
    public string? HeatRequirementLevel { get; set; }

    public double? RhizomeDepthLowCm { get; set; }
    public double? RhizomeDepthHighCm { get; set; }
    public bool? BarrierRequired { get; set; }
    public int? RecommendedBarrierDepthCm { get; set; }
    public string? SpreadRate { get; set; }
    public double? HedgeSpacingM { get; set; }

    public string? ShootEdibility { get; set; }
    public string? TimberStrength { get; set; }
    public string? ScentProfile { get; set; }
    public string? PrimaryUses { get; set; }

    public double? AnnualGrowthRateLowCm { get; set; }
    public double? AnnualGrowthRateHighCm { get; set; }
    public int? TimeToMaturityYearsLow { get; set; }
    public int? TimeToMaturityYearsHigh { get; set; }
    public int? TimeToHarvestYearsLow { get; set; }
    public int? TimeToHarvestYearsHigh { get; set; }

    public string? CulmColorPrimary { get; set; }
    public string? CulmColorSecondary { get; set; }
    public string? SeasonalColorChange { get; set; }
    public string? Texture { get; set; }
    public string? OrnamentalScore { get; set; }

    public int? ShootEmergenceStartMonth { get; set; }
    public int? ShootEmergenceEndMonth { get; set; }
    public int? DivisionStartMonth { get; set; }
    public int? DivisionEndMonth { get; set; }
    public int? TimberHarvestStartMonth { get; set; }
    public int? TimberHarvestEndMonth { get; set; }

    public string? Description { get; set; }
    public string? SwedishGrowerNotes { get; set; }
    public string? DataLimitations { get; set; }

    // Stored as JSON text for audit display.
    public string? ReviewNotesJson { get; set; }
    public string? MissingFieldsJson { get; set; }
    public string? ConflictFlagsJson { get; set; }
    public string? SourceRegistryKeysJson { get; set; }
    public string? FieldSourcesJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}