using System.ComponentModel;

namespace TownshipTales.Structures
{
    public enum LotClassEnum
    {
        [Description("Low")]
        Low,

        [Description("Low-Middle")]
        LowMiddle,

        [Description("Middle")]
        Middle,

        [Description("Middle-High")]
        MiddleHigh,

        [Description("High")]
        High,

        [Description("Special")]
        Special
    }
}
