using System;

namespace TownshipTales.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDebugString(this Enum e)
        {
            return $"{e.GetType().Name}.{e.ToString()}";
        }
    }
}
