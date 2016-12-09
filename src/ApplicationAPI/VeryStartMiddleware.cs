using System.Threading.Tasks;
using Microsoft.Owin;

namespace ApplicationAPI
{
    public class VeryStartMiddleware : BaseMiddleware
    {
        public VeryStartMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            return Next.Invoke(context);
        }
    }
}