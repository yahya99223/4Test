using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Producer.WebAPI;
[assembly: OwinStartup(typeof(Startup))]


namespace Producer.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            MessagingConfig.SetupBus();

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
