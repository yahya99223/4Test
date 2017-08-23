using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDScan.SaaS.SharedBlocks.Helpers.Core;

namespace Core
{
    public class SomeService : ISomeService
    {
        public SomeService()
        {
            Statistics.SomeServiceCount += 1;
        }

        public string SayHi()
        {
            var result = $"Requests Count: {Statistics.RequestsCount}, SomeService Count: {Statistics.SomeServiceCount}, AnotherService Count: {Statistics.AnotherServiceCount}, ServiceResolver Count: {Statistics.ServiceResolverCount}";
            var e1 = new MessageRaised(result);
            DomainEvents.Raise(e1);

            var e2 = new MessageProcessed(e1.Message);
            DomainEvents.Raise(e2);
            result = $"Requests Count: {Statistics.RequestsCount}, SomeService Count: {Statistics.SomeServiceCount}, AnotherService Count: {Statistics.AnotherServiceCount}, ServiceResolver Count: {Statistics.ServiceResolverCount}";
            return result;
        }
    }
}
