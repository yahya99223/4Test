using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using IoC;
using Microsoft.Owin;


namespace ApplicationAPI
{
    public class VeryStartMiddleware : BaseMiddleware
    {
        public VeryStartMiddleware(OwinMiddleware next) : base(next)
        {
        }


        public override async Task Invoke(IOwinContext context)
        {
            CallContext.LogicalSetData("CallContextId", Guid.NewGuid());
            var serviceResolver = ServiceResolverFactory.GetServiceResolver();
            var scope = serviceResolver.StartScope();
            await Next.Invoke(context);
            scope.Dispose();
        }
    }
}