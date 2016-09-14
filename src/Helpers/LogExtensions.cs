using System;
using System.Collections.Concurrent;
using log4net;

namespace Helpers
{
    /// <summary>
    /// Extensions to help make logging awesome - this should be installed into the root namespace of your application
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// Concurrent dictionary that ensures only one instance of a logger for a type.
        /// </summary>
        private static readonly ConcurrentDictionary<string, ILogService> _dictionary = new ConcurrentDictionary<string, ILogService>();

        public static ILogService LogFor<T>(this T type, Type forType)
        {
            string objectName = forType.GetFriendlyName();
            return Log(objectName);
        }

        public static ILogService Log<T>(this T type)
        {
            string objectName = typeof (T).GetFriendlyName();
            return Log(objectName);
        }

        public static ILogService Log(this string objectName)
        {
            return _dictionary.GetOrAdd(objectName, Helpers.Log.GetLoggerFor);
        }
    }
}