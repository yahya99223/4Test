using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            
            var message = new StringBuilder();
            message.AppendLine(string.Format("BeginWebRequests :{0}", StaticInfo.BeginWebRequests));
            message.AppendLine(string.Format("EndWebRequests :{0}", StaticInfo.EndWebRequests));
            message.AppendLine("   ");
            message.AppendLine(Environment.NewLine);
            message.AppendLine(string.Format("StartedUnitOfWorks :{0}", StaticInfo.StartedUnitOfWorks));
            //message.AppendLine(string.Format("CommitedUnitOfWorks :{0}", StaticInfo.CommitedUnitOfWorks));
            message.AppendLine(string.Format("DisposedUnitOfWorks :{0}", StaticInfo.DisposedUnitOfWorks));
            message.AppendLine("   ");
            message.AppendLine(string.Format("Exception :{0}", StaticInfo.Exception));
            //message.AppendLine(string.Format("Users :{0}", StaticInfo.Users));

            context.Response.Output.Write(message);
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