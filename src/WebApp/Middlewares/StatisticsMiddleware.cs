using System.Threading.Tasks;
using Core;
using Microsoft.Owin;

namespace WebApp.Middlewares
{
    public class StatisticsMiddleware : OwinMiddleware
    {
        public StatisticsMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            Statistics.RequestsCount += 1;
            await Next.Invoke(context);
        }
    }
}