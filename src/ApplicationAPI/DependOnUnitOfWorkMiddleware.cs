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

        public DependOnUnitOfWorkMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            var unitOfWork = ServiceResolver.Resolve<IUnitOfWork>();
            return Next.Invoke(context);
        }
    }
}