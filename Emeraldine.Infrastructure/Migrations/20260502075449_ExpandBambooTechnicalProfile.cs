using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeraldine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandBambooTechnicalProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BambooTechnicalProfiles_BambooSpeciesId",
                table: "BambooTechnicalProfiles");

            migrationBuilder.RenameColumn(
                name: "SwedishZone",
                table: "BambooTechnicalProfiles",
                newName: "WindTolerance");

            migrationBuilder.RenameColumn(
                name: "LightRequirement",
                table: "BambooTechnicalProfiles",
                newName: "WaterloggingTolerance");

            migrationBuilder.AddColumn<double>(
                name: "AdultHeightHighM",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AdultHeightLowM",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AnnualGrowthRateHighCm",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AnnualGrowthRateLowCm",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BarrierRequired",
                table: "BambooTechnicalProfiles",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColdWindSensitivity",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfidenceLevel",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConflictFlagsJson",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulmColorPrimary",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CulmColorSecondary",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CulmDiameterHighCm",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CulmDiameterLowCm",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataConfidence",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataLimitations",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionEndMonth",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionStartMonth",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DroughtTolerance",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldSourcesJson",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrowthHabit",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HardinessMaxC",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HardinessMinC",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeatRequirementLevel",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HedgeSpacingM",
                table: "BambooTechnicalProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LateFrostSensitivity",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LightFullSun",
                table: "BambooTechnicalProfiles",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LightPartialShade",
                table: "BambooTechnicalProfiles",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LightRequirementText",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LightShade",
                table: "BambooTechnicalProfiles",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MissingFieldsJson",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrnamentalScore",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedBarrierDepthCm",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewNotesJson",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeasonalColorChange",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShootEmergenceEndMonth",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShootEmergenceStartMonth",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SnowLoadTolerance",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDateBaseline",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceRegistryKeysJson",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpreadRate",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SwedishZoneHigh",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SwedishZoneLow",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Texture",
                table: "BambooTechnicalProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimberHarvestEndMonth",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimberHarvestStartMonth",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeToHarvestYearsHigh",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeToHarvestYearsLow",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeToMaturityYearsHigh",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeToMaturityYearsLow",
                table: "BambooTechnicalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BambooTechnicalProfiles_BambooSpeciesId",
                table: "BambooTechnicalProfiles",
                column: "BambooSpeciesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BambooTechnicalProfiles_BambooSpeciesId",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "AdultHeightHighM",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "AdultHeightLowM",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "AnnualGrowthRateHighCm",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "AnnualGrowthRateLowCm",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "BarrierRequired",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "ColdWindSensitivity",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "ConfidenceLevel",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "ConflictFlagsJson",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "CulmColorPrimary",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "CulmColorSecondary",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "CulmDiameterHighCm",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "CulmDiameterLowCm",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "DataConfidence",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "DataLimitations",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "DivisionEndMonth",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "DivisionStartMonth",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "DroughtTolerance",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "FieldSourcesJson",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "GrowthHabit",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "HardinessMaxC",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "HardinessMinC",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "HeatRequirementLevel",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "HedgeSpacingM",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "LateFrostSensitivity",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "LightFullSun",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "LightPartialShade",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "LightRequirementText",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "LightShade",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "MissingFieldsJson",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "OrnamentalScore",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "RecommendedBarrierDepthCm",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "ReviewNotesJson",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SeasonalColorChange",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "ShootEmergenceEndMonth",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "ShootEmergenceStartMonth",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SnowLoadTolerance",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SourceDateBaseline",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SourceRegistryKeysJson",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SpreadRate",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SwedishZoneHigh",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "SwedishZoneLow",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "Texture",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "TimberHarvestEndMonth",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "TimberHarvestStartMonth",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "TimeToHarvestYearsHigh",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "TimeToHarvestYearsLow",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "TimeToMaturityYearsHigh",
                table: "BambooTechnicalProfiles");

            migrationBuilder.DropColumn(
                name: "TimeToMaturityYearsLow",
                table: "BambooTechnicalProfiles");

            migrationBuilder.RenameColumn(
                name: "WindTolerance",
                table: "BambooTechnicalProfiles",
                newName: "SwedishZone");

            migrationBuilder.RenameColumn(
                name: "WaterloggingTolerance",
                table: "BambooTechnicalProfiles",
                newName: "LightRequirement");

            migrationBuilder.CreateIndex(
                name: "IX_BambooTechnicalProfiles_BambooSpeciesId",
                table: "BambooTechnicalProfiles",
                column: "BambooSpeciesId");
        }
    }
}
