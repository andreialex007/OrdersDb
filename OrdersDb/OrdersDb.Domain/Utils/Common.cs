using System;
using System.ComponentModel;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace OrdersDb.Domain.Utils
{
    public static class Common
    {
        public static T Convert<T>(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return default(T);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
                return (T)converter.ConvertFromString(input);

            return default(T);
        }

        public static object Convert(this string input, Type type)
        {
            if (string.IsNullOrEmpty(input))
                return type.GetDefault();

            var converter = TypeDescriptor.GetConverter(type);
            if (converter != null && converter.IsValid(input))
                return converter.ConvertFromString(input);

            return type.GetDefault();
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static bool IsNullOrZero(this int? i)
        {
            return i == null || i == 0;
        }

    }
}
