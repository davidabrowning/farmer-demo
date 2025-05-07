using System.Collections.Generic;

namespace FarmerDemo
{
    public static class CostCalculator
    {
        // Building construction
        public static List<ResourceAmount> FabricatorConstruction() => new() { new ResourceAmount(ResourceType.Twig, 5) };
        public static List<ResourceAmount> LaboratoryConstruction() => new() { new ResourceAmount(ResourceType.Berry, 6) };
        public static List<ResourceAmount> WoodBurnerConstruction() => new() { new ResourceAmount(ResourceType.Stone, 50) };
        public static List<ResourceAmount> CircuitMakerConstruction() => new() { new ResourceAmount(ResourceType.Berry, 1), new ResourceAmount(ResourceType.Iron, 8) };
        public static List<ResourceAmount> AutoHarvesterConstruction() => new() { new ResourceAmount(ResourceType.Berry, 5), new ResourceAmount(ResourceType.Circuit, 2) };
        public static List<ResourceAmount> SolarPanelConstruction() => new() { new ResourceAmount(ResourceType.Twig, 15), new ResourceAmount(ResourceType.Circuit, 5) };
        public static List<ResourceAmount> SeedSplicerConstruction() => new() { new ResourceAmount(ResourceType.Circuit, 3), new ResourceAmount(ResourceType.Stone, 5) };
        public static List<ResourceAmount> ARMConstruction() => new() { new ResourceAmount(ResourceType.Circuit, 50), new ResourceAmount(ResourceType.Iron, 50) };

        // Item costs
        public static List<ResourceAmount> BerryBasket() => new() { new ResourceAmount(ResourceType.Twig, 5) };
        public static List<ResourceAmount> Pickaxe() => new() { new ResourceAmount(ResourceType.Twig, 5), new ResourceAmount(ResourceType.Stone, 2) };

        // Resource conversions
        public static List<ResourceAmount> CircuitBatch() => new() { new ResourceAmount(ResourceType.Berry, 2), new ResourceAmount(ResourceType.Iron, 2) };
        public static List<ResourceAmount> SplicedSeeds() => new() { new ResourceAmount(ResourceType.Berry, 5), new ResourceAmount(ResourceType.Fish, 5), new ResourceAmount(ResourceType.Circuit, 1) };

        // StandardResearch costs
        public static ResourceAmount StandardResearch()
        {
            switch (EraManagerScript.Instance.CurrentEra)
            {
                case EraType.Survival:
                    return new ResourceAmount(ResourceType.Berry, 1);
                case EraType.Power:
                    return new ResourceAmount(ResourceType.Circuit, 1);
                case EraType.Automation:
                    return new ResourceAmount(ResourceType.Fish, 1);
                case EraType.ScientificAdvancement:
                    return new ResourceAmount(ResourceType.Seed, 1);
            }
            return null;
        }
        
        // Other
        public static List<ResourceAmount> ARMCure() => new() { new ResourceAmount(ResourceType.Seed, 3), new ResourceAmount(ResourceType.Circuit, 3) };
    }
}
