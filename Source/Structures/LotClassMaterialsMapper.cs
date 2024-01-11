using RimWorld;
using System.Collections.Generic;

namespace TownshipTales.Structures
{
    public static class LotClassMaterialsMapper
    {
        public static Dictionary<LotClassEnum, LotClassMaterials> LotClassMaterialsMap = new Dictionary<LotClassEnum, LotClassMaterials>()
        {
            // Low Class: Basic materials for the most economically challenged pawns, such as laborers or newly arrived colonists.
            { LotClassEnum.Low, new LotClassMaterials() { FloorMaterial = TerrainDefOf.Concrete, WallMaterial = ThingDefOf.WoodLog, DoorMaterial = ThingDefOf.WoodLog } },

            // Low Middle Class: A step up, suitable for pawns like skilled laborers or small shop owners.
            { LotClassEnum.LowMiddle, new LotClassMaterials() { FloorMaterial = TerrainDefOf.PavedTile, WallMaterial = ThingDefOf.WoodLog, DoorMaterial = ThingDefOf.WoodLog } },

            // Middle Class: Standard materials for the average colonist, like craftsmen or mid-level managers.
            { LotClassEnum.Middle, new LotClassMaterials() { FloorMaterial = TerrainDefOf.WoodPlankFloor, WallMaterial = ThingDefOf.BlocksGranite, DoorMaterial = ThingDefOf.WoodLog } },

            // Middle High Class: Higher quality for successful traders, senior managers, or skilled professionals.
            { LotClassEnum.MiddleHigh, new LotClassMaterials() { FloorMaterial = TerrainDefOf.WoodPlankFloor, WallMaterial = ThingDefOf.BlocksGranite, DoorMaterial = ThingDefOf.Steel } },

            // High Class: Luxurious materials for the wealthiest pawns, such as colony leaders or rich merchants.
            { LotClassEnum.High, new LotClassMaterials() { FloorMaterial = TerrainDefOf.TileSandstone, WallMaterial = ThingDefOf.BlocksGranite, DoorMaterial = ThingDefOf.Silver } },

            // Special Class: Exceptional quality for pawns of significant importance or unique status, like faction leaders or legendary figures.
            { LotClassEnum.Special, new LotClassMaterials() { FloorMaterial = TerrainDefOf.TileSandstone, WallMaterial = ThingDefOf.Plasteel, DoorMaterial = ThingDefOf.Gold } }
        };
    }
}
