using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Core;
using IDScan.VisionCortex.Shared.ResolverEngine.Windsor;
using IoC;

namespace ApplicationAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            StaticInfo.BeginWebRequests += 1;

            var serviceResolver = ServiceResolverFactory.GetServiceResolver();

            HttpContext context = HttpContext.Current;
            context.Items.Add("PerWebRequest", serviceResolver.BeginScope());
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            StaticInfo.EndWebRequests += 1;

            HttpContext context = HttpContext.Current;
            var scope = context.Items["PerWebRequest"] as IDisposable;
            scope.Dispose();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}