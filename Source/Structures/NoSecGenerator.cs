using RimWorld;
using System.Collections.Generic;
using System.Linq;
using TownshipTales.Configuration;
using TownshipTales.Entities;
using Verse;

namespace TownshipTales.Structures
{
    public static class NoSecGenerator
    {
        public static void Generate(Map map, CellRect noSecArea, IHouseFactory houseFactory)
        {
            int minDistanceBetweenHouses = NoSecConfig.MinDistanceBetweenHouses;
            int maxDistanceBetweenHouses = NoSecConfig.MaxDistanceBetweenHouses;

            int totalArea = noSecArea.Width * noSecArea.Height;
            int estimatedNumberOfHouses = totalArea / Rand.Range(minDistanceBetweenHouses, maxDistanceBetweenHouses);

            for (int i = 0; i < estimatedNumberOfHouses; i++)
            {
                // Create the house specification
                House house = houseFactory.CreateOneBedroomHouse();

                // Get a random position for the house
                IntVec3 randomPosition = FindRandomPositionForHouse(map, noSecArea, house);

                if (randomPosition == IntVec3.Invalid) continue; // No valid position found

                // Initialize the house
                house.Initialize(randomPosition);
                house.Build();
            }
        }

        private static IntVec3 FindRandomPositionForHouse(Map map, CellRect noSecArea, House house, int maxAttempts = 10)
        {
            if (maxAttempts <= 0) return IntVec3.Invalid; // Prevent infinite recursion

            int x = Rand.Range(noSecArea.minX, noSecArea.maxX);
            int z = Rand.Range(noSecArea.minZ, noSecArea.maxZ);
            IntVec3 randomPosition = new IntVec3(x, 0, z);
            IntVec2 propertySize = house.GardenSize;

            if (!randomPosition.InBounds(map) || !IsSuitableForBuilding(map, randomPosition, propertySize))
            {
                return FindRandomPositionForHouse(map, noSecArea, house, maxAttempts - 1);
            }

            return randomPosition;
        }

        private static bool IsSuitableForBuilding(Map map, IntVec3 position, IntVec2 buildingSize)
        {
            CellRect proposedArea = new CellRect(position.x, position.z, buildingSize.x, buildingSize.z);

            // Check if the entire area is within the map bounds
            if (!proposedArea.FullyContainedWithin(CellRect.WholeMap(map)))
            {
                return false;
            }

            // Check each cell in the proposed area for suitability
            foreach (IntVec3 cell in proposedArea)
            {
                TerrainDef terrain = map.terrainGrid.TerrainAt(cell);

                // Check for impassable terrain like water or mountains
                if (terrain.passability == Traversability.Impassable || terrain.IsWater)
                {
                    return false;
                }

                if (terrain == TerrainDefOf.WaterDeep) { return false; }
            }

            return true;
        }
    }
}
