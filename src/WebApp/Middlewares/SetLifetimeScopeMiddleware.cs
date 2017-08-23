using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Core;
using IDScan.SaaS.SharedBlocks.Helpers.Core;
using Microsoft.Owin;

namespace WebApp.Middlewares
{
    public class SetLifetimeScopeMiddleware : OwinMiddleware
    {
        private readonly ILifetimeScope lifetimeScope;

        public SetLifetimeScopeMiddleware(OwinMiddleware next, ILifetimeScope lifetimeScope) : base(next)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var scope = lifetimeScope.BeginLifetimeScope();
            await Next.Invoke(context);
            scope.Dispose();
        }
    }
}