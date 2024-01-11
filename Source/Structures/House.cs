using RimWorld;
using TownshipTales.Configuration;
using TownshipTales.Structures;
using Verse;

namespace TownshipTales.Entities
{
    public abstract class House
    {
        public CellRect HouseBounds { get; set; }
        public CellRect GardenBounds { get; set; }

        public ThingDef WallMaterial { get; set; } = ThingDefOf.WoodLog;
        public ThingDef DoorMaterial { get; set; } = ThingDefOf.WoodLog;
        public TerrainDef FloorMaterial { get; set; } = TerrainDefOf.WoodPlankFloor;

        public LotTypeEnum LotType { get; set; } = LotTypeEnum.Residential;
        public LotClassEnum LotClass { get; set; } = LotClassEnum.Middle;

        public IntVec2 GardenSize { get; set; } = new IntVec2(Rand.Range(NoSecConfig.MinGardenWidth, NoSecConfig.MaxGardenWidth), Rand.Range(NoSecConfig.MinGardenWidth, NoSecConfig.MaxGardenWidth));

        public House()
        {
            SetDoorMaterial(LotClassMaterialsMapper.LotClassMaterialsMap[LotClass].DoorMaterial);
            SetWallMaterial(LotClassMaterialsMapper.LotClassMaterialsMap[LotClass].WallMaterial);
            SetFloorMaterial(LotClassMaterialsMapper.LotClassMaterialsMap[LotClass].FloorMaterial);
        }

        public abstract IntVec2 CalculateHouseSize();

        public abstract void Initialize(IntVec3 position);

        public abstract void Build(Map map);

        public abstract void BuildHouse(Map map);

        public abstract void BuildRoom(Map map, CellRect room, ConnectionTypeEnum connectionType = ConnectionTypeEnum.None, ConnectionSideEnum doorSide = ConnectionSideEnum.None);

        public abstract void BuildGarden(Map map);

        private void SetWallMaterial(ThingDef material)
        {
            WallMaterial = material;
        }

        private void SetDoorMaterial(ThingDef material)
        {
            DoorMaterial = material;
        }

        private void SetFloorMaterial(TerrainDef material)
        {
            FloorMaterial = material;
        }

    }
}
