using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class StaticInfo
    {
        public static int BeginWebRequests { get; set; }
        public static int EndWebRequests { get; set; }
        public static int StartedUnitOfWorks { get; set; }
        public static int DisposedUnitOfWorks { get; set; }
        public static int CommitedUnitOfWorks { get; set; }
        public static int Users { get; set; }
    }
}
