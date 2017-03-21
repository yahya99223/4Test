using System.Threading.Tasks;
using Core.DataAccess;
using IoC;
using Microsoft.Owin;

namespace ApplicationAPI
{
    public class DependOnUnitOfWorkMiddleware : OwinMiddleware
    {

        public DependOnUnitOfWorkMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var serviceResolver = ServiceResolverFactory.GetServiceResolver();
            var unitOfWork = serviceResolver.Resolve<IUnitOfWork>();

            await Next.Invoke(context);
        }
    }
}