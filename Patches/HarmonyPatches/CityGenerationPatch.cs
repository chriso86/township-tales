using HarmonyLib;
using Verse;
using Rimworld;
using RimWorld.Planet;
using TownshipTales.Configuration;

namespace TownshipTales
{
    [HarmonyPatch(typeof(MapGenerator), "GenerateMap")]
    public static class CityGenerationPatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref Map __result)
        {
            if (Current.Game.Scenario.name == "TownshipTales")
            {
                GenerateCity(__result);
            }
        }

        public static void GenerateCity(Map map)
        {
            CellRect cityBounds = GetCityBounds(map);
            var (urbanCenter, suburbanArea, ruralArea, noSecArea) = DivideCityRegions(cityBounds, map);

            ClearArea(map, cityBounds);
            GenerateUrbanCenter(map, urbanCenter);
            GenerateSuburbanArea(map, suburbanArea);
            GenerateRuralArea(map, ruralArea);
            GenerateNoSecArea(map, noSecArea);
        }

        private static CellRect GetCityBounds(Map map)
        {
            // Get the size of the map
            IntVec3 mapSize = map.Size;
            int mapWidth = mapSize.x;
            int mapHeight = mapSize.z;

            // Calculate the maximum offset for the city and buffer
            int buffer = 10; // Minimum distance from the map edge
            int maxOffsetX = mapWidth - cityWidth - buffer;
            int maxOffsetY = mapHeight - cityHeight - buffer;

            // Ensure the buffer is respected
            maxOffsetX = Math.Max(buffer, maxOffsetX);
            maxOffsetY = Math.Max(buffer, maxOffsetY);

            // Calculate the size of the town/city
            int cityWidth = (int)(mapWidth * Rand.Range(CityConfig.MinCitySizePercentage, CityConfig.MaxCitySizePercentage));
            int cityHeight = (int)(mapHeight * Rand.Range(CityConfig.MinCitySizePercentage, CityConfig.MaxCitySizePercentage));

            // Offset the city to "lean" to one side of the map
            // Randomize the offset with the buffer
            int offsetX = Rand.Range(buffer, maxOffsetX);
            int offsetY = Rand.Range(buffer, maxOffsetY);

            // Define the city bounds
            return new CellRect(offsetX, offsetY, cityWidth, cityHeight);
        }

        private static (CellRect innerRegion, CellRect middleRegion, CellRect outerRegion, CellRect noSecArea) DivideCityRegions(CellRect cityBounds, Map map)
        {
            // Urban Center: 20%, Suburban Area: 40%, Rural Area: 40%
            int urbanWidth = (int)(cityBounds.Width * CityConfig.UrbanCenterPercentage);
            int suburbanWidth = (int)(cityBounds.Width * CityConfig.SuburbanAreaPercentage);
            // Ensure the total width does not exceed cityBounds.Width due to rounding
            int ruralWidth = cityBounds.Width - urbanWidth - suburbanWidth;

            CellRect urbanCenter = new CellRect(cityBounds.minX, cityBounds.minZ, urbanWidth, cityBounds.Height);
            CellRect suburbanArea = new CellRect(cityBounds.minX + urbanWidth, cityBounds.minZ, suburbanWidth, cityBounds.Height);
            CellRect ruralArea = new CellRect(cityBounds.minX + urbanWidth + suburbanWidth, cityBounds.minZ, ruralWidth, cityBounds.Height);

            // NoSec Area: The remainder of the map outside the city bounds
            CellRect noSecArea = new CellRect(0, 0, map.Size.x, map.Size.z);
            noSecArea = CellRect.WholeMap(map).Except(cityBounds);

            return (urbanCenter, suburbanArea, ruralArea, noSecArea);
        }

        private static void GenerateInnerCity(Map map, CellRect region)
        {
            // Logic for high-density commercial buildings
        }

        private static void GenerateMiddleCity(Map map, CellRect region)
        {
            // Logic for medium-density artisan buildings
        }

        private static void GenerateOuterCity(Map map, CellRect region)
        {
            // Logic for low-density housing and farmlands
        }

        private static void GenerateNoSecArea(Map map, CellRect region)
        {
            // Logic for outlier farmlands and other elements in the "risky" area outside city bounds
        }

        private static void ClearArea(Map map, CellRect area)
        {
            foreach (IntVec3 cell in area)
            {
                List<Thing> thingsInCell = map.thingGrid.ThingsListAt(cell).ToList();

                foreach (Thing thing in thingsInCell)
                {
                    // Check if the thing is a building, corpse, or junk (non-plant, non-filth)
                    if (thing.def.category == ThingCategory.Building ||
                        thing is Corpse ||
                        (thing.def.category == ThingCategory.Item && !(thing is Filth || thing is Plant || thing.def.defName.Contains("Chunk"))))
                    {
                        thing.Destroy(DestroyMode.Vanish);
                    }
                }
            }
        }
    }
}
