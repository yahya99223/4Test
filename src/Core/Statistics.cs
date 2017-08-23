using IDScan.SaaS.SharedBlocks.Helpers.Core;

namespace Core
{
    public class ServiceResolverFactory
    {
        public static IServiceResolver Instance { get; set; }
    }

    public static class Statistics
    {
        public static int RequestsCount = 0;
        public static int SomeServiceCount = 0;
        public static int AnotherServiceCount = 0;
        public static int ServiceResolverCount = 0;
    }
}