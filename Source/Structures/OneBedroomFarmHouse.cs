using RimWorld;
using System;
using TownshipTales.Entities;
using Verse;

namespace TownshipTales.Structures
{
    public class OneBedroomFarmHouse : House
    {
        public CellRect Bedroom { get; set; }
        public CellRect Bathroom { get; set; }
        public CellRect KitchenDining { get; set; }
        public CellRect LivingRoom { get; set; }
        public CellRect Hallway { get; set; }
        public CellRect StoreRoom { get; set; }

        public IntVec2 BedroomSize { get; set; } = new IntVec2(Rand.RangeInclusive(7, 10), Rand.RangeInclusive(8, 11));
        public IntVec2 BathroomSize { get; set; } = new IntVec2(Rand.RangeInclusive(3, 4), Rand.RangeInclusive(3, 5));
        public IntVec2 KitchenDiningSize { get; set; } = new IntVec2(Rand.RangeInclusive(7, 10), Rand.RangeInclusive(8, 11));
        public IntVec2 LivingRoomSize { get; set; } = new IntVec2(Rand.RangeInclusive(7, 10), Rand.RangeInclusive(8, 11));
        public IntVec2 HallwaySize { get; set; } = new IntVec2(Rand.RangeInclusive(3, 5), Rand.RangeInclusive(3, 5));
        public IntVec2 StoreRoomSize { get; set; } = new IntVec2(Rand.RangeInclusive(3, 5), Rand.RangeInclusive(3, 5));

        public OneBedroomFarmHouse() : base()
        {
            LotClass = LotClassEnum.Middle;

            // Randomize between agricultural and residential
            LotType = Rand.Range(0, 2) == 0 ? LotTypeEnum.Agricultural : LotTypeEnum.Residential;
        }

        public override IntVec2 CalculateHouseSize()
        {
            int width = LivingRoomSize.x + KitchenDiningSize.x + StoreRoomSize.x;
            int height = Math.Max(LivingRoomSize.z, BedroomSize.z + BathroomSize.z + HallwaySize.z);

            return new IntVec2(width, height);
        }

        /*
         * Here's a simple approach to arranging these rooms:
         * 
         * - Living Room at the front as the main entrance area.
         * - Kitchen and Dining area adjacent to the living room, creating a large, open communal space.
         * - Bedroom located behind or next to the living area for privacy.
         * - Bathroom accessible via the bedroom or hallway for convenience.
         * - Hallway connecting the living area to the bedroom and bathroom.
         * - Store Room near the kitchen or hallway for easy access.
         **/
        public override void Initialize(IntVec3 position)
        {
            // Calculate the size of the house
            IntVec2 houseSize = CalculateHouseSize();

            // Calculate the bounds of the house
            HouseBounds = new CellRect(position.x, position.z, houseSize.x, houseSize.z);

            // Calculate the bounds of the garden
            GardenBounds = new CellRect(position.x - GardenSize.x, position.z - GardenSize.z,
                                houseSize.x + GardenSize.x, houseSize.z + GardenSize.z);

            // Living Room at the front as the main entrance area
            LivingRoom = new CellRect(position.x, position.z, LivingRoomSize.x, LivingRoomSize.z);

            // Kitchen and Dining area adjacent to the living room
            KitchenDining = new CellRect(position.x + LivingRoomSize.x, position.z, KitchenDiningSize.x, KitchenDiningSize.z);

            // Store Room near the kitchen or hallway for easy access
            StoreRoom = new CellRect(position.x + LivingRoomSize.x + KitchenDiningSize.x, position.z, StoreRoomSize.x, StoreRoomSize.z);

            // Hallway connecting the living area to the bedroom and bathroom
            Hallway = new CellRect(position.x, position.z + LivingRoomSize.z, HallwaySize.x, HallwaySize.z);

            // Bedroom located behind or next to the living area for privacy
            Bedroom = new CellRect(position.x + HallwaySize.x, position.z + LivingRoomSize.z, BedroomSize.x, BedroomSize.z);

            // Bathroom accessible via the bedroom or hallway for convenience
            Bathroom = new CellRect(position.x + HallwaySize.x + BedroomSize.x, position.z + LivingRoomSize.z, BathroomSize.x, BathroomSize.z);
        }

        public override void Build(Map map)
        {
            BuildHouse(map);
            BuildGarden(map);
        }

        public override void BuildHouse(Map map)
        {
            // Build the house
            BuildRoom(map, LivingRoom, ConnectionTypeEnum.Door, ConnectionSideEnum.Left);
            BuildRoom(map, KitchenDining, ConnectionTypeEnum.OpenPlan, ConnectionSideEnum.Left);
            BuildRoom(map, StoreRoom, ConnectionTypeEnum.Door, ConnectionSideEnum.Left);
            BuildRoom(map, Hallway, ConnectionTypeEnum.Door, ConnectionSideEnum.Top);
            BuildRoom(map, Bedroom, ConnectionTypeEnum.Door, ConnectionSideEnum.Left);
            BuildRoom(map, Bathroom, ConnectionTypeEnum.Door, ConnectionSideEnum.Left);
        }

        public override void BuildRoom(Map map, CellRect room, ConnectionTypeEnum connectionType = ConnectionTypeEnum.None, ConnectionSideEnum doorSide = ConnectionSideEnum.None)
        {
            // Build the walls
            foreach (IntVec3 cell in room.Cells)
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.Concrete);


            }

            // Build the floor
            foreach (IntVec3 cell in room.EdgeCells)
            {
                map.terrainGrid.SetTerrain(cell, TerrainDefOf.Concrete);
            }
        }

        public override void BuildGarden(Map map)
        {
            throw new NotImplementedException();
        }
    }
}
