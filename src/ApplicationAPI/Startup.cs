using System;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using IDScan.VisionCortex.Shared.ResolverEngine.Windsor;
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
            HttpConfiguration config = new HttpConfiguration();

            InitializeServiceResolver(config);
            OnAppDisposing(app);

            webApiConfigRegister(config);
            GlobalConfiguration.Configure(webApiConfigRegister);
            app.Use<DependOnUnitOfWorkMiddleware>();
            app.Use<AnotherDependOnUnitOfWorkMiddleware>();
            app.UseWebApi(config);
        }


        private static void InitializeServiceResolver(HttpConfiguration config)
        {
            var executeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            var serviceResolver = ServiceResolverFactory.GetServiceResolver(executeFolder);

            var httpDependencyResolver = serviceResolver.Resolve<System.Web.Http.Dependencies.IDependencyResolver>();
            config.DependencyResolver = httpDependencyResolver;

            /*var signalRDependencyResolver = serviceResolver.Resolve<Microsoft.AspNet.SignalR.IDependencyResolver>();
            GlobalHost.DependencyResolver = signalRDependencyResolver;*/
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

            // config.EnableSwagger("docs/{apiVersion}", c => c.SingleApiVersion("v1", "First Version"));

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            //config.Filters.Add(new AuthorizeAttribute());
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
                    ServiceResolverFactory.GetServiceResolver().Stop();
                });
            }
        }
    }
}