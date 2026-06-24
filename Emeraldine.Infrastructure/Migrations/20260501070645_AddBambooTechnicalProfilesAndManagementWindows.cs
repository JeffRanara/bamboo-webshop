using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeraldine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBambooTechnicalProfilesAndManagementWindows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BambooManagementWindows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BambooSpeciesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskType = table.Column<int>(type: "integer", nullable: false),
                    StartMonth = table.Column<int>(type: "integer", nullable: false),
                    StartDay = table.Column<int>(type: "integer", nullable: true),
                    EndMonth = table.Column<int>(type: "integer", nullable: false),
                    EndDay = table.Column<int>(type: "integer", nullable: true),
                    RecommendedCulmAgeYearsLow = table.Column<int>(type: "integer", nullable: true),
                    RecommendedCulmAgeYearsHigh = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BambooManagementWindows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BambooManagementWindows_BambooSpecies_BambooSpeciesId",
                        column: x => x.BambooSpeciesId,
                        principalTable: "BambooSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BambooTechnicalProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BambooSpeciesId = table.Column<Guid>(type: "uuid", nullable: false),
                    NativeRange = table.Column<string>(type: "text", nullable: true),
                    FloweringIntervalYearsLow = table.Column<int>(type: "integer", nullable: true),
                    FloweringIntervalYearsHigh = table.Column<int>(type: "integer", nullable: true),
                    WallThicknessLowCm = table.Column<double>(type: "double precision", nullable: true),
                    WallThicknessHighCm = table.Column<double>(type: "double precision", nullable: true),
                    InternodeLengthLowCm = table.Column<double>(type: "double precision", nullable: true),
                    InternodeLengthHighCm = table.Column<double>(type: "double precision", nullable: true),
                    LeafLengthLowCm = table.Column<double>(type: "double precision", nullable: true),
                    LeafLengthHighCm = table.Column<double>(type: "double precision", nullable: true),
                    LeafWidthLowCm = table.Column<double>(type: "double precision", nullable: true),
                    LeafWidthHighCm = table.Column<double>(type: "double precision", nullable: true),
                    SheathLengthLowCm = table.Column<double>(type: "double precision", nullable: true),
                    SheathLengthHighCm = table.Column<double>(type: "double precision", nullable: true),
                    SheathWidthLowCm = table.Column<double>(type: "double precision", nullable: true),
                    SheathWidthHighCm = table.Column<double>(type: "double precision", nullable: true),
                    SwedishZone = table.Column<string>(type: "text", nullable: true),
                    LightRequirement = table.Column<string>(type: "text", nullable: true),
                    SoilType = table.Column<string>(type: "text", nullable: true),
                    SoilPhLow = table.Column<double>(type: "double precision", nullable: true),
                    SoilPhHigh = table.Column<double>(type: "double precision", nullable: true),
                    MoisturePreference = table.Column<string>(type: "text", nullable: true),
                    RhizomeDepthLowCm = table.Column<double>(type: "double precision", nullable: true),
                    RhizomeDepthHighCm = table.Column<double>(type: "double precision", nullable: true),
                    ShootEdibility = table.Column<string>(type: "text", nullable: true),
                    TimberStrength = table.Column<string>(type: "text", nullable: true),
                    ScentProfile = table.Column<string>(type: "text", nullable: true),
                    PrimaryUses = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SwedishGrowerNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BambooTechnicalProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BambooTechnicalProfiles_BambooSpecies_BambooSpeciesId",
                        column: x => x.BambooSpeciesId,
                        principalTable: "BambooSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BambooManagementWindows_BambooSpeciesId",
                table: "BambooManagementWindows",
                column: "BambooSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_BambooTechnicalProfiles_BambooSpeciesId",
                table: "BambooTechnicalProfiles",
                column: "BambooSpeciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BambooManagementWindows");

            migrationBuilder.DropTable(
                name: "BambooTechnicalProfiles");
        }
    }
}
