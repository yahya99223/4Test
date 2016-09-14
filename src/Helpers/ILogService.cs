using System;
using System.Runtime.ExceptionServices;

namespace Helpers
{
    /// <summary>
    ///     Ensures a default constructor for the logger type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILogService<T> //where T : new()
    {
    }

    public interface ILogService
    {
        /// <summary>
        ///     Initializes the instance for the logger name
        /// </summary>
        /// <param name="loggerName">Name of the logger</param>
        void InitializeFor(string loggerName);

        /// <summary>
        ///     Debug level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        void Debug(string message, Exception exception = null);

        /// <summary>
        ///     Info level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        void Info(string message, Exception exception = null);

        /// <summary>
        ///     Warn level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        void Warn(string message, Exception exception = null);

        /// <summary>
        ///     Error level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        void Error(string message, Exception exception = null);

        /// <summary>
        ///     Fatal level of the specified message. The other method is preferred since the execution is deferred.
        /// </summary>
        void Fatal(string message, Exception exception = null);
    }
}