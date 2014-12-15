using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Hosting;
using OrdersDb.Domain.Services._Common.Entities;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace OrdersDb.Domain.Utils
{
    public static class Common
    {
        public static T Convert<T>(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return default(T);

            var converter = TypeDescriptor.GetConverter(typeof (T));
            if (converter != null)
                return (T) converter.ConvertFromString(input);

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

        public static bool NullOrNoId(this EntityBase entityBase)
        {
            if (entityBase == null)
                return true;
            if (entityBase.Id == 0)
                return true;
            return false;
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

        public static string GetPropertyName<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof (TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda,
                    type));

            return propInfo.Name;
        }


        public static string GetPropertyName<TSource, TProperty>(
            this TSource source,
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof (TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda,
                    type));

            return propInfo.Name;
        }

        public static string GetTemporaryFolder()
        {
            return HostingEnvironment.MapPath("~/Files/");
        }
    }
}