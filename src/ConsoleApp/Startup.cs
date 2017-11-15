using System.Threading;
using System.Web.Http;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Cors;
using Owin;

namespace ConsoleApp
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            OnAppDisposing(app);
            
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }




        private static void OnAppDisposing(IAppBuilder app)
        {
            var properties = new AppProperties(app.Properties);
            CancellationToken token = properties.OnAppDisposing;
            if (token != CancellationToken.None)
            {
                token.Register(() =>
                {
                    //No need to pass folder because we should've been already created the instance
                });
            }
        }
    }
}