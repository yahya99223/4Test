using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Core.DataAccess;
using Microsoft.Owin;
using Microsoft.Owin.Logging;

namespace ApplicationAPI
{
    public class DependOnUnitOfWorkMiddleware : BaseMiddleware
    {
        private IUnitOfWork unitOfWork;

        public DependOnUnitOfWorkMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            unitOfWork = ServiceResolver.Resolve<IUnitOfWork>();
            await Next.Invoke(context);
        }
    }
}