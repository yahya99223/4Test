using Core;
using IDScan.VisionCortex.Shared.ResolverEngine.Windsor;
using Microsoft.Owin;

namespace ApplicationAPI
{
    public abstract class BaseMiddleware : OwinMiddleware
    {
        protected readonly IServiceResolver ServiceResolver;

        protected BaseMiddleware(OwinMiddleware next) : base(next)
        {
            ServiceResolver = ServiceResolverFactory.GetServiceResolver();
        }
    }
}