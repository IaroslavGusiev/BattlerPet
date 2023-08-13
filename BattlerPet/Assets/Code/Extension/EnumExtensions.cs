using System;
using System.Linq;

namespace CodeBase.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt<TEnum>(this TEnum value) where TEnum : Enum => 
            Convert.ToInt32(value);

        public static TEnum ToEnum<TEnum>(this int value)
        {
            if (typeof(TEnum).IsEnumDefined(value))
                return (TEnum)(object)value;

            return default;
        }
        
        public static T GetRandomEnumValue<T>(this T enumType, params T[] excludeValues) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            values = values.Except(excludeValues).ToArray();
            return values.PickRandom();
        }
    }
}