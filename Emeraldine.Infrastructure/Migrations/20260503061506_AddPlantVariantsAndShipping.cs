using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeraldine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlantVariantsAndShipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BambooSpeciesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PotSizeLiters = table.Column<int>(type: "integer", nullable: false),
                    PriceExVat = table.Column<decimal>(type: "numeric", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    EstimatedPlantHeightLowCm = table.Column<int>(type: "integer", nullable: true),
                    EstimatedPlantHeightHighCm = table.Column<int>(type: "integer", nullable: true),
                    RequiresCulmTrimmingForShipping = table.Column<bool>(type: "boolean", nullable: false),
                    VariantNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantVariants_BambooSpecies_BambooSpeciesId",
                        column: x => x.BambooSpeciesId,
                        principalTable: "BambooSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantVariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeightKg = table.Column<double>(type: "double precision", nullable: false),
                    PackageLengthCm = table.Column<int>(type: "integer", nullable: false),
                    PackageWidthCm = table.Column<int>(type: "integer", nullable: false),
                    PackageHeightCm = table.Column<int>(type: "integer", nullable: false),
                    ShippingCategory = table.Column<int>(type: "integer", nullable: false),
                    CanShip = table.Column<bool>(type: "boolean", nullable: false),
                    CanPickup = table.Column<bool>(type: "boolean", nullable: false),
                    ShippingNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingProfiles_PlantVariants_PlantVariantId",
                        column: x => x.PlantVariantId,
                        principalTable: "PlantVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantVariants_BambooSpeciesId",
                table: "PlantVariants",
                column: "BambooSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProfiles_PlantVariantId",
                table: "ShippingProfiles",
                column: "PlantVariantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingProfiles");

            migrationBuilder.DropTable(
                name: "PlantVariants");
        }
    }
}
