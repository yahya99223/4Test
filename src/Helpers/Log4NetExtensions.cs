/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Helpers
{
    public static class Log4NetExtensions
    {
        public static ILog Log(this string objectName)
        {
            var logger = log4net.LogManager.GetLogger(objectName);
            return logger;
        }

        public static ILog Log<T>(this T type)
        {
            var logger = log4net.LogManager.GetLogger(typeof (T));
            return logger;
        }
    }
}
*/
