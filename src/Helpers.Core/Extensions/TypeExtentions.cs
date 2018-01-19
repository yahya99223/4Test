using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace Helpers.Core
{
    public static class TypeExtentions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof (TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }


        /// <summary>
        ///     Determines if a type is numeric.  Nullable numeric types are considered numeric.
        /// </summary>
        /// <remarks>
        ///     Boolean is not considered numeric.
        /// </remarks>
        public static bool IsNumeric(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
                    {
                        return IsNumeric(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }


        public static T ToNullable<T>(this IConvertible obj)
        {
            var t = typeof (T);
            var u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                return (obj == null) ? default(T) : (T) Convert.ChangeType(obj, u);
            }
            return (T) Convert.ChangeType(obj, t);
        }

     
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));
            return (ExpandoObject) expando;
        }
    }
}