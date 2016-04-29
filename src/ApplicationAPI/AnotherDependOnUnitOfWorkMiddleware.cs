using System.Threading.Tasks;
using Core.DataAccess;
using Microsoft.Owin;

namespace ApplicationAPI
{
    public class AnotherDependOnUnitOfWorkMiddleware : BaseMiddleware
    {
        private IUnitOfWork unitOfWork;

        public AnotherDependOnUnitOfWorkMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            unitOfWork = ServiceResolver.Resolve<IUnitOfWork>();
           return Next.Invoke(context);
        }
    }
}