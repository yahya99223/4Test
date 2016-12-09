using System.Threading.Tasks;
using Core.DataAccess;
using Microsoft.Owin;

namespace ApplicationAPI
{
    public class AnotherDependOnUnitOfWorkMiddleware : BaseMiddleware
    {

        public AnotherDependOnUnitOfWorkMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            var unitOfWork = ServiceResolver.Resolve<IUnitOfWork>();
           return Next.Invoke(context);
        }
    }
}