using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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

            //Assume that we have some Authentication here 
            //Then we want to do something like this 
            context.Request.User = new GenericPrincipal(new GenericIdentity("Sameer"), new[] {"Administrator"});

            //context.Request.User = new GenericPrincipal(new ClaimsIdentity(new List<Claim> {new Claim(ClaimTypes.Name, "Sameer")}), new[] {"Administrator"});

            Helpers.Write("Owin Middleware:", context.Request.User);

            await next(env);
        }
    }
}