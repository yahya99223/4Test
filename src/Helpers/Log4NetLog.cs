using System;
using System.Globalization;
using System.Runtime;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Util;

[assembly: XmlConfigurator(Watch = true)]

namespace Helpers
{
    /// <summary>
    ///     Log4net logger implementing special ILog class
    /// </summary>
    public sealed class Log4NetLog : ILogService, ILogService<Log4NetLog>
    {
        // ignore Log4NetLog in the call stack
        private static readonly Type _declaringType = typeof (Log4NetLog);
        private ILog _logger;

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void InitializeFor(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Debug(string message, Exception exception = null)
        {
            if (_logger.IsDebugEnabled) Log(Level.Debug, message, exception);
        }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Info(string message, Exception exception = null)
        {
            if (_logger.IsInfoEnabled) Log(Level.Info, message, exception);
        }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Warn(string message, Exception exception = null)
        {
            if (_logger.IsWarnEnabled) Log(Level.Warn, message, exception);
        }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Error(string message, Exception exception = null)
        {
            // don't check for enabled at this level
            Log(Level.Error, message, exception);
        }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Fatal(string message, Exception exception = null)
        {
            // don't check for enabled at this level
            Log(Level.Fatal, message, exception);
        }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private void Log(Level level, Func<string> message)
        {
            Log(level, message(), null);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private void Log(Level level, Func<string> message, Exception exception)
        {
            _logger.Logger.Log(_declaringType, level, message(), exception);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private void Log(Level level, string message, Exception exception = null)
        {
            // SystemStringFormat is used to evaluate the message as late as possible. A filter may discard this message.
            _logger.Logger.Log(_declaringType, level, new SystemStringFormat(CultureInfo.CurrentCulture, message), exception);
        }
    }
}