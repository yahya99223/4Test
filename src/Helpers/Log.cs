using System;

namespace Helpers
{
    public static class Log
    {
        private static Type _logType;
        private static ILogService _testLogger;

        /// <summary>
        ///     Sets up logging to be with a certain type
        /// </summary>
        /// <typeparam name="T">The type of ILog for the application to use</typeparam>
        public static void InitializeWith<T>() where T : ILogService, new()
        {
            _logType = typeof (T);
        }

        /// <summary>
        ///     Initializes a new instance of a logger for an object.
        ///     This should be done only once per object name.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>ILog instance for an object if log type has been intialized; otherwise null</returns>
        public static ILogService GetLoggerFor(string objectName)
        {
            if (_testLogger != null) return _testLogger;

            var logger = Activator.CreateInstance(_logType) as ILogService;
            if (logger != null)
            {
                logger.InitializeFor(objectName);
            }

            return logger;
        }
    }
}