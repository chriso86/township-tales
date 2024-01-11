using System.ComponentModel;

namespace TownshipTales.Structures
{
    public enum ConnectionSideEnum
    {
        None = 0,

        [Description("Left")]
        Left,

        [Description("Right")]
        Right,

        [Description("Top")]
        Top,

        [Description("Bottom")]
        Bottom
    }
}
