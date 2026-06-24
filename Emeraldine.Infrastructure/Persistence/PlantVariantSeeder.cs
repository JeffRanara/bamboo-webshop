using Emeraldine.Domain.Entities;
using Emeraldine.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Emeraldine.Infrastructure.Persistence;

public static class PlantVariantSeeder
{
    public static async Task SeedAsync(BambooEnterpriseDbContext context)
    {
        if (await context.PlantVariants.AnyAsync())
        {
            return;
        }

        var species = await context.BambooSpecies.ToListAsync();

        foreach (var bamboo in species)
        {
            var potSizes = GetPotSizesForSpecies(bamboo);

            foreach (var potSize in potSizes)
            {
                var variant = CreateVariant(bamboo, potSize);

                context.PlantVariants.Add(variant);
            }
        }

        await context.SaveChangesAsync();
    }

    private static int[] GetPotSizesForSpecies(BambooSpecies species)
    {
        var isLargeRunner =
            species.Genus == "Phyllostachys" &&
            species.HeightHigh >= 10;

        var isMediumRunner =
            species.Genus == "Phyllostachys" &&
            species.HeightHigh < 10;

        var isClumping =
            species.Genus == "Fargesia";

        var isGroundcover =
            species.Genus == "Sasa";

        if (isLargeRunner)
        {
            return new[] { 5, 10, 25, 50 };
        }

        if (isMediumRunner)
        {
            return new[] { 5, 10, 25 };
        }

        if (isClumping)
        {
            return new[] { 1, 5, 10, 25 };
        }

        if (isGroundcover)
        {
            return new[] { 1, 5, 10 };
        }

        return new[] { 5, 10, 25 };
    }

    private static PlantVariant CreateVariant(BambooSpecies species, int potSizeLiters)
    {
        var price = GetPriceExVat(species, potSizeLiters);
        var estimatedHeight = GetEstimatedPlantHeight(species, potSizeLiters);
        var shipping = CreateShippingProfile(species, potSizeLiters);

        return new PlantVariant
        {
            BambooSpeciesId = species.Id,
            PotSizeLiters = potSizeLiters,
            PriceExVat = price,
            StockQuantity = GetInitialStock(species, potSizeLiters),
            IsAvailable = true,
            EstimatedPlantHeightLowCm = estimatedHeight.low,
            EstimatedPlantHeightHighCm = estimatedHeight.high,
            RequiresCulmTrimmingForShipping = shipping.RequiresTrimming,
            VariantNotes = GetVariantNotes(species, potSizeLiters),

            ShippingProfile = new ShippingProfile
            {
                WeightKg = shipping.WeightKg,
                PackageLengthCm = shipping.LengthCm,
                PackageWidthCm = shipping.WidthCm,
                PackageHeightCm = shipping.HeightCm,
                ShippingCategory = shipping.Category,
                CanShip = shipping.CanShip,
                CanPickup = true,
                ShippingNotes = shipping.Notes
            }
        };
    }

    private static decimal GetPriceExVat(BambooSpecies species, int potSizeLiters)
    {
        var basePrice = potSizeLiters switch
        {
            1 => 120m,
            5 => 280m,
            10 => 520m,
            25 => 1150m,
            50 => 2200m,
            _ => potSizeLiters * 55m
        };

        var rarityMultiplier =
            species.Genus == "Fargesia" && species.Cultivar is not null ? 1.15m :
            species.Species.Contains("Jiuzhaigou") ? 1.25m :
            species.Genus == "Bashania" ? 1.20m :
            species.Genus == "Semiarundinaria" ? 1.15m :
            1.0m;

        var timberMultiplier =
            species.HeightHigh >= 10 ? 1.15m : 1.0m;

        return Math.Round(basePrice * rarityMultiplier * timberMultiplier, 0);
    }

    private static int GetInitialStock(BambooSpecies species, int potSizeLiters)
    {
        if (potSizeLiters == 50)
        {
            return 1;
        }

        if (potSizeLiters == 25)
        {
            return 2;
        }

        if (species.Genus == "Fargesia")
        {
            return potSizeLiters <= 5 ? 8 : 4;
        }

        return potSizeLiters <= 5 ? 6 : 3;
    }

    private static (int low, int high) GetEstimatedPlantHeight(BambooSpecies species, int potSizeLiters)
    {
        return potSizeLiters switch
        {
            1 => (25, 60),
            5 => (60, 120),
            10 => (100, 180),
            25 => (160, 300),
            50 => (250, 450),
            _ => (50, 150)
        };
    }

    private static string GetVariantNotes(BambooSpecies species, int potSizeLiters)
    {
        if (potSizeLiters >= 25)
        {
            return "Large nursery plant. Pickup recommended; shipping may require culm trimming.";
        }

        if (potSizeLiters <= 5)
        {
            return "Young plant suitable for establishment, collection building, or economical shipping.";
        }

        return "Established plant size suitable for faster garden impact.";
    }

    private static ShippingDraft CreateShippingProfile(BambooSpecies species, int potSizeLiters)
    {
        return potSizeLiters switch
        {
            1 => new ShippingDraft(
                WeightKg: 1.5,
                LengthCm: 80,
                WidthCm: 20,
                HeightCm: 20,
                Category: ShippingCategory.SmallParcel,
                CanShip: true,
                RequiresTrimming: false,
                Notes: "Usually shippable as whole young plant."
            ),

            5 => new ShippingDraft(
                WeightKg: 6,
                LengthCm: 120,
                WidthCm: 30,
                HeightCm: 30,
                Category: ShippingCategory.MediumParcel,
                CanShip: true,
                RequiresTrimming: false,
                Notes: "Usually shippable whole if culms are flexible and tied."
            ),

            10 => new ShippingDraft(
                WeightKg: 12,
                LengthCm: 150,
                WidthCm: 40,
                HeightCm: 40,
                Category: ShippingCategory.BulkyParcel,
                CanShip: true,
                RequiresTrimming: species.HeightHigh >= 8,
                Notes: species.HeightHigh >= 8
                    ? "May require light culm trimming for parcel shipment."
                    : "Bulky but generally shippable."
            ),

            25 => new ShippingDraft(
                WeightKg: 30,
                LengthCm: 180,
                WidthCm: 55,
                HeightCm: 55,
                Category: ShippingCategory.FreightOrPickup,
                CanShip: true,
                RequiresTrimming: true,
                Notes: "Freight or pickup recommended. Parcel shipping likely requires culm trimming."
            ),

            50 => new ShippingDraft(
                WeightKg: 65,
                LengthCm: 220,
                WidthCm: 75,
                HeightCm: 75,
                Category: ShippingCategory.PickupOnly,
                CanShip: false,
                RequiresTrimming: true,
                Notes: "Pickup only in this initial model. Customer should arrange suitable transport."
            ),

            _ => new ShippingDraft(
                WeightKg: potSizeLiters * 1.2,
                LengthCm: 120,
                WidthCm: 40,
                HeightCm: 40,
                Category: ShippingCategory.BulkyParcel,
                CanShip: true,
                RequiresTrimming: true,
                Notes: "Custom pot size; shipping profile should be reviewed manually."
            )
        };
    }

    private record ShippingDraft(
        double WeightKg,
        int LengthCm,
        int WidthCm,
        int HeightCm,
        ShippingCategory Category,
        bool CanShip,
        bool RequiresTrimming,
        string Notes
    );
}