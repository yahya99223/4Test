using System.Threading.Tasks;
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
            using (var scope = ServiceResolver.SetMiddlewareScope())
            {
                await Next.Invoke(context);
            }
        }
    }
}