using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Consumer.WebAPI.Startup))]


namespace Consumer.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
                config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            });

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(GlobalConfiguration.Configuration);
        }
    }
}
