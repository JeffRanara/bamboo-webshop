using Emeraldine.Domain.Entities;
using Emeraldine.Domain.Enums;

namespace Emeraldine.Infrastructure.Persistence;

public static class BambooSpeciesSeeder
{
    public static async Task SeedAsync(BambooEnterpriseDbContext context)
    {
        if (context.BambooSpecies.Any())
            return;

        var species = new List<BambooSpecies>
        {
            new() { Genus = "Phyllostachys", Species = "vivax", Cultivar = "Huanwenzhu", CommonName = "Huanwenzhu bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 8, HeightHigh = 12, DiameterLow = 5, DiameterHigh = 10, HardinessLow = -21, TypicalShootingMonth = 5, Notes = "Green culm with yellow sulcus. Snow breakage risk." },
            new() { Genus = "Phyllostachys", Species = "vivax", Cultivar = "McClure", CommonName = "Giant timber bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 8, HeightHigh = 14, DiameterLow = 6, DiameterHigh = 12, HardinessLow = -21, TypicalShootingMonth = 5, Notes = "Classic green timber bamboo with large leaves." },
            new() { Genus = "Phyllostachys", Species = "vivax", Cultivar = "Aureocaulis", CommonName = "Golden vivax bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 8, HeightHigh = 12, DiameterLow = 6, DiameterHigh = 12, HardinessLow = -21, TypicalShootingMonth = 5, Notes = "Yellow culms with green striping. High ornamental value." },

            new() { Genus = "Phyllostachys", Species = "parvifolia", CommonName = "Hardy timber bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 6, HeightHigh = 10, DiameterLow = 4, DiameterHigh = 8, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Extremely hardy timber-type bamboo with edible shoots." },
            new() { Genus = "Phyllostachys", Species = "atrovaginata", CommonName = "Incense bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 5, HeightHigh = 8, DiameterLow = 3, DiameterHigh = 6, HardinessLow = -23, TypicalShootingMonth = 5, Notes = "Air canals for wet soil. Scented culms." },

            new() { Genus = "Phyllostachys", Species = "aureosulcata", CommonName = "Yellow-groove bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 7, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Green culm with yellow sulcus. Zig-zag lower culms." },
            new() { Genus = "Phyllostachys", Species = "aureosulcata", Cultivar = "Aureocaulis", CommonName = "Golden yellow-groove bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 7, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Solid yellow culms. Very hardy." },
            new() { Genus = "Phyllostachys", Species = "aureosulcata", Cultivar = "Spectabilis", CommonName = "Spectabilis bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 7, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Yellow culm with green sulcus. Red shoots." },
            new() { Genus = "Phyllostachys", Species = "aureosulcata", Cultivar = "Argus", CommonName = "Argus bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 7, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Green striping on yellow culms." },

            new() { Genus = "Phyllostachys", Species = "nigra", CommonName = "Black bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 3, HeightHigh = 5, DiameterLow = 2, DiameterHigh = 4, HardinessLow = -18, TypicalShootingMonth = 5, Notes = "Jet black culms. Needs shelter." },
            new() { Genus = "Phyllostachys", Species = "nigra", Cultivar = "Henonis", CommonName = "Henon bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 6, HeightHigh = 10, DiameterLow = 4, DiameterHigh = 8, HardinessLow = -23, TypicalShootingMonth = 5, Notes = "Giant grey-green timber bamboo. Very hardy." },
            new() { Genus = "Phyllostachys", Species = "nigra", Cultivar = "Punctata", CommonName = "Speckled black bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 7, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -20, TypicalShootingMonth = 5, Notes = "Speckled dark culms. Refined ornamental character." },

            new() { Genus = "Phyllostachys", Species = "bissetii", CommonName = "Bisset bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 7, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -26, TypicalShootingMonth = 5, Notes = "Excellent hardy windbreak bamboo." },
            new() { Genus = "Phyllostachys", Species = "decora", CommonName = "Beautiful bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 6, DiameterLow = 2, DiameterHigh = 4, HardinessLow = -22, TypicalShootingMonth = 5, Notes = "Drought tolerant running bamboo." },
            new() { Genus = "Phyllostachys", Species = "nuda", CommonName = "Nude sheath bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 4, HeightHigh = 6, DiameterLow = 2, DiameterHigh = 4, HardinessLow = -26, TypicalShootingMonth = 5, Notes = "Early purple-black nodes. Very hardy." },

            new() { Genus = "Sasa", Species = "kurilensis", CommonName = "Kuril bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 1, HeightHigh = 2, DiameterLow = 0.5, DiameterHigh = 1.5, HardinessLow = -30, TypicalShootingMonth = 5, Notes = "Northern species with large leaves." },
            new() { Genus = "Bashania", Species = "qingchengshanensis", CommonName = "Qingchengshan bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 2, HeightHigh = 4, DiameterLow = 1, DiameterHigh = 3, HardinessLow = -20, TypicalShootingMonth = 5, Notes = "Strong stiff culms." },
            new() { Genus = "Semiarundinaria", Species = "fastuosa", CommonName = "Temple bamboo", Habit = GrowthHabit.Monopodial, HeightLow = 5, HeightHigh = 8, DiameterLow = 2, DiameterHigh = 5, HardinessLow = -20, TypicalShootingMonth = 5, Notes = "Upright columnar habit." },

            new() { Genus = "Fargesia", Species = "nitida x murieliae", Cultivar = "Obelisk", CommonName = "Obelisk bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 4, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Tall upright clumping hedge bamboo." },
            new() { Genus = "Fargesia", Species = "murieliae", Cultivar = "Red Zebra", CommonName = "Red Zebra bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 3, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -23, TypicalShootingMonth = 5, Notes = "Dark red and green contrast stems." },
            new() { Genus = "Fargesia", Species = "nitida x murieliae", Cultivar = "Schensbossen", CommonName = "Schensbossen bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 4, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -26, TypicalShootingMonth = 5, Notes = "Exceptional hardiness." },
            new() { Genus = "Fargesia", Species = "sp.", Cultivar = "Rufa", CommonName = "Rufa bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 1.5, HeightHigh = 2.5, DiameterLow = 0.8, DiameterHigh = 1.5, HardinessLow = -23, TypicalShootingMonth = 4, Notes = "Non-curling leaves. Early shoots." },
            new() { Genus = "Fargesia", Species = "robusta", Cultivar = "Campbell", CommonName = "Campbell bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 3, HeightHigh = 5, DiameterLow = 1, DiameterHigh = 2.5, HardinessLow = -20, TypicalShootingMonth = 4, Notes = "Early shoots with strong white culm sheath pattern." },
            new() { Genus = "Fargesia", Species = "nitida", Cultivar = "Jürgen", CommonName = "Jürgen bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 3.5, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -26, TypicalShootingMonth = 5, Notes = "Upright habit with dark culms." },
            new() { Genus = "Fargesia", Species = "sp. Jiuzhaigou", Cultivar = "1", CommonName = "Red Panda bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 3, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Cherry-red stems in sun." },
            new() { Genus = "Fargesia", Species = "sp. Jiuzhaigou", Cultivar = "Deep Purple", CommonName = "Deep Purple bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 3, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Deep purple culm coloration." },
            new() { Genus = "Fargesia", Species = "demissa", Cultivar = "Gerry", CommonName = "Gerry bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 3, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -25, TypicalShootingMonth = 5, Notes = "Almost black culms." },
            new() { Genus = "Fargesia", Species = "denudata", Cultivar = "Xian 2", CommonName = "Xian 2 bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 4, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -22, TypicalShootingMonth = 5, Notes = "Elegant cloud-like habit." },
            new() { Genus = "Fargesia", Species = "sp.", Cultivar = "KR5287", CommonName = "KR5287 bamboo", Habit = GrowthHabit.Sympodial, HeightLow = 2, HeightHigh = 4, DiameterLow = 1, DiameterHigh = 2, HardinessLow = -20, TypicalShootingMonth = 5, Notes = "Rare collector bamboo with bluish culms." }
        };
        context.BambooSpecies.AddRange(species);
        await context.SaveChangesAsync();

        // --- Technical profile seed (example: Phyllostachys vivax 'Aureocaulis')
        /**
        var vivax = context.BambooSpecies
            .FirstOrDefault(s => s.Genus == "Phyllostachys" && s.Species == "vivax");

        if (vivax != null && !context.BambooTechnicalProfiles.Any(p => p.BambooSpeciesId == vivax.Id))
        {
            var profile = new BambooTechnicalProfile
            {
                BambooSpeciesId = vivax.Id,

                NativeRange = "Eastern China",
                FloweringIntervalYearsLow = 60,
                FloweringIntervalYearsHigh = 120,

                WallThicknessLowCm = 0.5,
                WallThicknessHighCm = 0.9,
                InternodeLengthLowCm = 25,
                InternodeLengthHighCm = 35,

                LeafLengthLowCm = 10,
                LeafLengthHighCm = 18,
                LeafWidthLowCm = 1.5,
                LeafWidthHighCm = 2.5,

                SheathLengthLowCm = 12,
                SheathLengthHighCm = 18,

                SwedishZone = "1–2 (3 sheltered)",
                LightRequirement = "Full sun to partial shade",
                SoilType = "Loam / garden soil",
                SoilPhLow = 5.5,
                SoilPhHigh = 7.0,
                MoisturePreference = "Moist, well-drained",

                RhizomeDepthLowCm = 20,
                RhizomeDepthHighCm = 40,

                ShootEdibility = "Excellent",
                TimberStrength = "Low (decorative only)",
                ScentProfile = "Sweet/grassy when cut",
                PrimaryUses = "Decorative, edible shoots",

                Description = "Large, elegant bamboo with bright yellow culms and occasional green striping.",
                SwedishGrowerNotes = "Thin culms prone to snow breakage; shake snow in winter and plant in sheltered locations."
            };

            context.BambooTechnicalProfiles.Add(profile);
            await context.SaveChangesAsync();
        } **/
    }
}