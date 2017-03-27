using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Microsoft.Owin;


namespace ApplicationAPI
{
    public class VeryStartMiddleware : OwinMiddleware
    {
        public VeryStartMiddleware(OwinMiddleware next) : base(next)
        {
        }


        public override async Task Invoke(IOwinContext context)
        {
            var watch = Stopwatch.StartNew();

            await Next.Invoke(context);

            watch.Stop();
            var processTime = watch.Elapsed.TotalSeconds;
        }
    }
}