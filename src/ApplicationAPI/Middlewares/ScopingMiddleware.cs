using System;
using System.Threading;
using System.Threading.Tasks;
using IoC;
using Microsoft.Owin;

namespace ApplicationAPI
{
    public class ScopingMiddleware : OwinMiddleware
    {
        public ScopingMiddleware(OwinMiddleware next) : base(next)
        {
        }


        public override async Task Invoke(IOwinContext context)
        {
            var serviceResolver = ServiceResolverFactory.GetServiceResolver();
            var requestScope = serviceResolver.StartScope();
            await Next.Invoke(context);
            requestScope.Dispose();
        }
    }
}