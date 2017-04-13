using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Application.API.Startup))]

namespace Application.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
            });

            //app.MapSignalR();

            SignalRConfig.Register(app);

        }
    }
}
