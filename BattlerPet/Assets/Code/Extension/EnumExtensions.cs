using System;

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
    }
}