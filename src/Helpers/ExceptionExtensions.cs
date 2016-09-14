using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            if (type == typeof(int))
                return "int";
            else if (type == typeof(short))
                return "short";
            else if (type == typeof(byte))
                return "byte";
            else if (type == typeof(bool))
                return "bool";
            else if (type == typeof(long))
                return "long";
            else if (type == typeof(float))
                return "float";
            else if (type == typeof(double))
                return "double";
            else if (type == typeof(decimal))
                return "decimal";
            else if (type == typeof(string))
                return "string";
            else if (type.IsGenericType)
                return type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(x => GetFriendlyName(x)).ToArray()) + ">";
            else
                return type.Name;
        }
    }

    public static class ExceptionExtensions
    {
        public static string GetDetailedMessage(this Exception exception)
        {
            string message = null;
            if (exception != null)
            {
                message = getNestedMessage(exception.InnerException, exception.Message);
            }
            return message;
        }

        private static string getNestedMessage(Exception exception, string message)
        {
            if (exception != null)
            {
                message = message + Environment.NewLine + " ... Inner message: " + exception.Message;
                if (exception.InnerException != null)
                {
                    return getNestedMessage(exception.InnerException, message);
                }
            }
            return message;
        }
    }
}
