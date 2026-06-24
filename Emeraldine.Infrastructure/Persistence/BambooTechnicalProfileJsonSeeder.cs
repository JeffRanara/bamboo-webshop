using System.Text.Json;
using Emeraldine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emeraldine.Infrastructure.Persistence;

public static class BambooTechnicalProfileJsonSeeder
{
    public static async Task SeedAsync(BambooEnterpriseDbContext context)
    {
        var filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Persistence",
            "SeedData",
            "bamboo_profiles_sourced_enriched_v1.json"
        );

        if (!File.Exists(filePath))
        {
            return;
        }

        var json = await File.ReadAllTextAsync(filePath);
        using var document = JsonDocument.Parse(json);

        var root = document.RootElement;
        var profiles = root.GetProperty("profiles");
        var sourceRegistry = root.GetProperty("sourceRegistry").GetRawText();

        foreach (var profileElement in profiles.EnumerateArray())
        {
            var identity = profileElement.GetProperty("identity");

            var genus = GetString(identity, "genus");
            var species = GetString(identity, "species");
            var cultivar = GetString(identity, "cultivar");

            var bambooSpecies = await context.BambooSpecies
                .FirstOrDefaultAsync(s =>
                    s.Genus == genus &&
                    s.Species == species &&
                    s.Cultivar == cultivar);

            if (bambooSpecies == null)
            {
                continue;
            }

            var existing = await context.BambooTechnicalProfiles
                .FirstOrDefaultAsync(p => p.BambooSpeciesId == bambooSpecies.Id);

            var profile = existing ?? new BambooTechnicalProfile
            {
                BambooSpeciesId = bambooSpecies.Id
            };

            MapProfile(profile, profileElement, sourceRegistry);

            if (existing == null)
            {
                context.BambooTechnicalProfiles.Add(profile);
            }
        }

        await context.SaveChangesAsync();
    }

    private static void MapProfile(
        BambooTechnicalProfile profile,
        JsonElement root,
        string sourceRegistryJson)
    {
        var identity = root.GetProperty("identity");
        var taxonomy = root.GetProperty("taxonomy");
        var morphology = root.GetProperty("morphology");
        var climate = root.GetProperty("climateAndSoil");
        var grower = root.GetProperty("growerMetrics");
        var aesthetic = root.GetProperty("aestheticSensory");
        var calendar = root.GetProperty("managementCalendar");
        var notes = root.GetProperty("notes");
        var audit = root.GetProperty("sourceAudit");

        profile.SourceDateBaseline = GetString(identity, "sourceDateBaseline");
        profile.DataConfidence = GetString(identity, "dataConfidence");
        profile.ConfidenceLevel = GetString(audit, "confidenceLevel");

        profile.NativeRange = GetString(taxonomy, "nativeRange");
        profile.FloweringIntervalYearsLow = GetInt(taxonomy, "floweringIntervalYearsLow");
        profile.FloweringIntervalYearsHigh = GetInt(taxonomy, "floweringIntervalYearsHigh");

        profile.GrowthHabit = GetString(morphology, "growthHabit");
        profile.AdultHeightLowM = GetDouble(morphology, "adultHeightLowM");
        profile.AdultHeightHighM = GetDouble(morphology, "adultHeightHighM");
        profile.CulmDiameterLowCm = GetDouble(morphology, "culmDiameterLowCm");
        profile.CulmDiameterHighCm = GetDouble(morphology, "culmDiameterHighCm");
        profile.WallThicknessLowCm = GetDouble(morphology, "wallThicknessLowCm");
        profile.WallThicknessHighCm = GetDouble(morphology, "wallThicknessHighCm");
        profile.InternodeLengthLowCm = GetDouble(morphology, "internodeLengthLowCm");
        profile.InternodeLengthHighCm = GetDouble(morphology, "internodeLengthHighCm");

        profile.LeafLengthLowCm = GetDouble(morphology, "leafLengthLowCm");
        profile.LeafLengthHighCm = GetDouble(morphology, "leafLengthHighCm");
        profile.LeafWidthLowCm = GetDouble(morphology, "leafWidthLowCm");
        profile.LeafWidthHighCm = GetDouble(morphology, "leafWidthHighCm");
        profile.SheathLengthLowCm = GetDouble(morphology, "sheathLengthLowCm");
        profile.SheathLengthHighCm = GetDouble(morphology, "sheathLengthHighCm");
        profile.SheathWidthLowCm = GetDouble(morphology, "sheathWidthLowCm");
        profile.SheathWidthHighCm = GetDouble(morphology, "sheathWidthHighCm");

        profile.HardinessMinC = GetDouble(climate, "hardinessMinC");
        profile.HardinessMaxC = GetDouble(climate, "hardinessMaxC");
        profile.SwedishZoneLow = GetInt(climate, "swedishZoneLow");
        profile.SwedishZoneHigh = GetInt(climate, "swedishZoneHigh");

        profile.LightFullSun = GetBool(climate, "lightFullSun");
        profile.LightPartialShade = GetBool(climate, "lightPartialShade");
        profile.LightShade = GetBool(climate, "lightShade");
        profile.LightRequirementText = GetString(climate, "lightRequirementText");

        profile.SoilType = GetString(climate, "soilType");
        profile.SoilPhLow = GetDouble(climate, "soilPhLow");
        profile.SoilPhHigh = GetDouble(climate, "soilPhHigh");
        profile.MoisturePreference = GetString(climate, "moisturePreference");

        profile.DroughtTolerance = GetString(climate, "droughtTolerance");
        profile.WaterloggingTolerance = GetString(climate, "waterloggingTolerance");
        profile.WindTolerance = GetString(climate, "windTolerance");
        profile.SnowLoadTolerance = GetString(climate, "snowLoadTolerance");
        profile.ColdWindSensitivity = GetString(climate, "coldWindSensitivity");
        profile.LateFrostSensitivity = GetString(climate, "lateFrostSensitivity");
        profile.HeatRequirementLevel = GetString(climate, "heatRequirementLevel");

        profile.RhizomeDepthLowCm = GetDouble(grower, "rhizomeDepthLowCm");
        profile.RhizomeDepthHighCm = GetDouble(grower, "rhizomeDepthHighCm");
        profile.BarrierRequired = GetBool(grower, "barrierRequired");
        profile.RecommendedBarrierDepthCm = GetInt(grower, "recommendedBarrierDepthCm");
        profile.SpreadRate = GetString(grower, "spreadRate");
        profile.HedgeSpacingM = GetDouble(grower, "hedgeSpacingM");

        profile.ShootEdibility = GetString(grower, "shootEdibility");
        profile.TimberStrength = GetString(grower, "timberStrength");
        profile.ScentProfile = GetString(grower, "scentProfile");
        profile.PrimaryUses = GetString(grower, "primaryUses");

        profile.AnnualGrowthRateLowCm = GetDouble(grower, "annualGrowthRateLowCm");
        profile.AnnualGrowthRateHighCm = GetDouble(grower, "annualGrowthRateHighCm");
        profile.TimeToMaturityYearsLow = GetInt(grower, "timeToMaturityYearsLow");
        profile.TimeToMaturityYearsHigh = GetInt(grower, "timeToMaturityYearsHigh");
        profile.TimeToHarvestYearsLow = GetInt(grower, "timeToHarvestYearsLow");
        profile.TimeToHarvestYearsHigh = GetInt(grower, "timeToHarvestYearsHigh");

        profile.CulmColorPrimary = GetString(aesthetic, "culmColorPrimary");
        profile.CulmColorSecondary = GetString(aesthetic, "culmColorSecondary");
        profile.SeasonalColorChange = GetString(aesthetic, "seasonalColorChange");
        profile.Texture = GetString(aesthetic, "texture");
        profile.OrnamentalScore = GetString(aesthetic, "ornamentalScore");

        profile.ShootEmergenceStartMonth = GetInt(calendar, "shootEmergenceStartMonth");
        profile.ShootEmergenceEndMonth = GetInt(calendar, "shootEmergenceEndMonth");
        profile.DivisionStartMonth = GetInt(calendar, "divisionStartMonth");
        profile.DivisionEndMonth = GetInt(calendar, "divisionEndMonth");
        profile.TimberHarvestStartMonth = GetInt(calendar, "timberHarvestStartMonth");
        profile.TimberHarvestEndMonth = GetInt(calendar, "timberHarvestEndMonth");

        profile.Description = GetString(notes, "description");
        profile.SwedishGrowerNotes = GetString(notes, "swedishGrowerNotes");
        profile.DataLimitations = GetString(notes, "dataLimitations");

        profile.ReviewNotesJson = GetRawJson(notes, "reviewNotes");
        profile.MissingFieldsJson = GetRawJson(audit, "missingFields");
        profile.ConflictFlagsJson = GetRawJson(audit, "conflictFlags");
        profile.SourceRegistryKeysJson = GetRawJson(audit, "sourceRegistryKeys");
        profile.FieldSourcesJson = GetRawJson(audit, "fieldSources");
        // Full source registry is currently kept in the JSON file itself.

        profile.UpdatedAt = DateTime.UtcNow;
    }

    private static string? GetString(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) ||
            property.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return property.GetString();
    }

    private static double? GetDouble(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) ||
            property.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return property.GetDouble();
    }

    private static int? GetInt(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) ||
            property.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return property.GetInt32();
    }

    private static bool? GetBool(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) ||
            property.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return property.GetBoolean();
    }

    private static string? GetRawJson(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) ||
            property.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return property.GetRawText();
    }
}