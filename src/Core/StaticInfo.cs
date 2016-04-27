using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class StaticInfo
    {
        public static int OwinContexts { get; set; }
        public static int WebRequests { get; set; }
        public static int UnitOfWorks { get; set; }
        public static int Users { get; set; }
    }
}
