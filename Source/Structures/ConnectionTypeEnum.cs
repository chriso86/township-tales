using System.ComponentModel;

namespace TownshipTales.Structures
{
    public enum ConnectionTypeEnum
    {
        None = 0,

        [Description("Door")]
        Door,

        [Description("Open Plan")]
        OpenPlan
    }
}
