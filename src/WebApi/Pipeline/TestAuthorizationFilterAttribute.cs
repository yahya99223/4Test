using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace WebApi
{
    //public class TestAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    public class TestAuthorizationFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }


        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            Helpers.Write("AuthorizationFilter", actionContext.RequestContext.Principal);

            return true;
            //return base.IsAuthorized(actionContext);
        }
    }
}