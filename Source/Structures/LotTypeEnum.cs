using System.ComponentModel;

namespace TownshipTales.Structures
{
    public enum LotTypeEnum
    {
        None = 0,

        [Description("Residential")]
        Residential,

        [Description("Commercial")]
        Commercial,

        [Description("Industrial")]
        Industrial,

        [Description("Agricultural")]
        Agricultural,

        [Description("Public")]
        Public,

        [Description("Special")]
        Special
    }
}
