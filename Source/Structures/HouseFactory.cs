using TownshipTales.Entities;

namespace TownshipTales.Structures
{
    public class FarmHouseFactory : IHouseFactory
    {
        public House CreateOneBedroomHouse()
        {
            return new OneBedroomFarmHouse();
        }
    }
}
