using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;


namespace WebApi
{
    public class TestAuthenticationFilter : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;


        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
                           {
                               //Helpers.Write("AuthenticationFilter", context.ActionContext.RequestContext.Principal);
                               Helpers.Write("AuthenticationFilter", context.Principal);
                           });
        }


        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {

        }
    }
}