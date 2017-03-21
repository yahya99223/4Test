using System;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using IoC;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Newtonsoft.Json.Serialization;
using Owin;


[assembly: OwinStartup(typeof(ApplicationAPI.Startup))]
namespace ApplicationAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
                                          {
                                              InitializeServiceResolver(config);
                                              webApiConfigRegister(config);
                                          });

            OnAppDisposing(app);

            app.Use<VeryStartMiddleware>();
            //app.Use<DependOnUnitOfWorkMiddleware>();
            //app.Use<AnotherDependOnUnitOfWorkMiddleware>();
            app.UseWebApi(GlobalConfiguration.Configuration);
        }


        private static void InitializeServiceResolver(HttpConfiguration config)
        {
            var executeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            var serviceResolver = ServiceResolverFactory.GetServiceResolver(executeFolder);

            var httpDependencyResolver = serviceResolver.Resolve<System.Web.Http.Dependencies.IDependencyResolver>();
            config.DependencyResolver = httpDependencyResolver;
        }


        private static void webApiConfigRegister(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                      name: "DefaultApi",
                      routeTemplate: "api/{controller}/{id}",
                      defaults: new {controller = "Default", id = RouteParameter.Optional}
                  );


            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

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
                    ServiceResolverFactory.GetServiceResolver().Dispose();
                });
            }
        }
    }
}