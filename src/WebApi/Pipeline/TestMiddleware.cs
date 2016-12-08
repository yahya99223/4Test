using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;


namespace WebApi
{
    public class TestMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> next;


        public TestMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            this.next = next;
        }


        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            Helpers.Write("Owin Middleware:", context.Request.User);

            await next(env);
        }
    }
}