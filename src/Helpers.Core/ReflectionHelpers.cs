using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Helpers.Core
{
    public static class ReflectionHelpers
    {
        /// <summary>
        ///     Get the property path from given lambda as string
        /// </summary>
        /// <typeparam name="T">The type to get one of its properties path </typeparam>
        /// <typeparam name="TProperty">The wanted property to get its path</typeparam>
        /// <param name="selector">Lambda expression to retrieve the property path from</param>
        /// <returns>String that represent the property path</returns>
        public static string GetPropertyPath<T, TProperty>(Expression<Func<T, TProperty>> selector)
        {
            var sb = new StringBuilder();
            var memberExpr = selector.Body as MemberExpression;
            if (memberExpr == null)
            {
                var unaryExpression = selector.Body as UnaryExpression;
                if (unaryExpression != null)
                    memberExpr = unaryExpression.Operand as MemberExpression;
            }
            while (memberExpr != null)
            {
                var name = memberExpr.Member.Name;
                if (sb.Length > 0)
                    name = name + ".";
                sb.Insert(0, name);
                if (memberExpr.Expression is ParameterExpression)
                    return sb.ToString();
                memberExpr = memberExpr.Expression as MemberExpression;
            }

            var constantExpression = selector.Body as ConstantExpression;
            if (constantExpression != null)
                return constantExpression.Value as string;

            return null;
            //throw new ArgumentException("The expression must be a MemberExpression, UnaryExpression or ConstantExpression", "selector");
        }


        public static Type GetPropertyType<T, TProperty>(Expression<Func<T, TProperty>> selector)
        {
            return GetPropertyType(typeof(T), GetPropertyPath(selector));
        }


        public static Type GetPropertyType<T>(string propertyPath)
        {
            return GetPropertyType(typeof(T), propertyPath);
        }


        /// <summary>
        ///     Get the type of the sub property no matter what level of the property is or the value is null or not null
        /// </summary>
        /// <param name="baseType">the base type to start from it</param>
        /// <param name="propertyPath">the path to the </param>
        /// <returns></returns>
        public static Type GetPropertyType(Type baseType, string propertyPath)
        {
            if (baseType == null)
                throw new ArgumentNullException("baseType");

            var neededHierarchy = propertyPath.Split('.');
            for (var i = 0; i < neededHierarchy.Count(); i++)
            {
                if (baseType.GetProperties().All(p => p.Name != neededHierarchy[i]))
                    throw new Exception("The Type \"" + baseType + "\" does NOT contain property named : \"" + neededHierarchy[i] + "\"");

                baseType = baseType.GetProperty(neededHierarchy[i]).PropertyType;
            }
            return baseType;
        }

        public static object GetPropertyValue<T, TProperty>(T instance, Expression<Func<T, TProperty>> selector)
        {
            var path = GetPropertyPath(selector);
            return GetPropertyValue(instance, path);
        }

        /// <summary>
        ///     Get the value of passed value's property by passing the hierarchy of it
        /// </summary>
        /// <param name="instance">The object to get property from</param>
        /// <param name="property">Property hierarchy as string separated with '.'</param>
        /// <returns>The property value as object</returns>        
        public static object GetPropertyValue(object instance, string property)
        {
            var neededHierarchy = property.Split('.');
            var neededValue = instance;
            for (var i = 0; i < neededHierarchy.Count(); i++)
            {
                if (neededValue == null)
                    continue;
                var props = TypeDescriptor.GetProperties(neededValue, true);
                if (props.Count > 0 && props.Find(neededHierarchy[i], true) != null)
                {
                    neededValue = props.Count > 0
                        ? props.Find(neededHierarchy[i], true).GetValue(neededValue)
                        : null;
                }
                else
                {
                    neededValue = property;
                }
            }
            return neededValue;
        }


        /// <summary>
        ///     Get Enum values as dictionary
        /// </summary>
        /// <typeparam name="K"> </typeparam>
        /// <returns> Dictionary with string key and int value </returns>
        public static IEnumerable<KeyValuePair<string, int>> EnumToIntDictionary<K>()
        {
            if (typeof(K).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            return Enum.GetValues(typeof(K)).Cast<int>().ToDictionary(item => Enum.GetName(typeof(K), item));
        }


        /// <summary>
        ///     Get Enum values as Dictionary with string key and string value
        /// </summary>
        /// <typeparam name="K"> </typeparam>
        /// <returns> Dictionary with string key and string value </returns>
        public static IEnumerable<KeyValuePair<string, string>> EnumToStringDictionary<K>()
        {
            if (typeof(K).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            return Enum.GetValues(typeof(K)).Cast<int>().ToDictionary(item => Enum.GetName(typeof(K), item), item => item.ToString());
        }


        /// <summary>
        ///     Try to get value from resource file
        /// </summary>
        /// <param name="resourceType"> The type of resource file</param>
        /// <param name="resourceKey"> Resource Key</param>
        /// <returns></returns>
        public static string GetResourceValue(Type resourceType, string resourceKey)
        {
            var property = resourceType.GetProperty(resourceKey, BindingFlags.Static | BindingFlags.Public);
            if (property != null)
            {
                return (string) property.GetValue(property.DeclaringType, null);
            }
            return null;
        }



        /// <summary>
        ///     Maps from enum to enum
        /// </summary>
        /// <typeparam name="T">Target enum type</typeparam>
        /// <param name="value">Source enum value</param>
        /// <returns> Nullable enum </returns>
        public static T? GetNullableEnumValue<T>(string value) where T : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return GetEnumValue<T>(value);
        }



        /// <summary>
        ///     Maps from object to enum value
        /// </summary>
        /// <typeparam name="T">Target enum type</typeparam>
        /// <param name="value">Source value</param>
        /// <returns>Enum</returns>
        public static T GetEnumValue<T>(object value) where T : struct
        {
            if (value == null)
                throw new Exception($"value can't be null. You should pass value to cast to the {typeof(T).Name} enum.");
            return GetEnumValue<T>(value.ToString());
        }

        /// <summary>
        ///     Maps from string to enum
        /// </summary>
        /// <typeparam name="T">Target enum type</typeparam>
        /// <param name="value">Source value</param>
        /// <returns>Enum</returns>
        public static T GetEnumValue<T>(string value) where T : struct
        {
            T targetType;
            if (Enum.TryParse(value, out targetType))
            {
                return targetType;
            }
            throw new Exception($"There in no item with value {value} in {typeof(T).Name} enum");
        }

        /// <summary>
        ///      Checks if Enum Value is in the valid range.
        /// </summary>
        /// <param name="enumType">Type of the Enumeration</param>
        /// <param name="enumValue">the value of the enum object.</param>
        /// <returns></returns>
        public static bool IsValidEnumValue(Type enumType, object enumValue)
        {
            if (!enumType.IsEnum)
                return false;

            var enumName = Enum.GetName(enumType, enumValue);
            return enumName != null;
        }
    }
}