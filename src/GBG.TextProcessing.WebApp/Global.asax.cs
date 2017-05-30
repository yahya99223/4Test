using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using NServiceBus;
using WebGrease.Configuration;

namespace GBG.TextProcessing.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            configureEndpoint();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        private void configureEndpoint()
        {
            Console.Title = "TextProcessing.WebApp";

            var endpointConfiguration = new EndpointConfiguration("TextProcessing.ConsoleApp");
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

            var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterInstance(endpointInstance);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            var mvcContainer = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(mvcContainer));
        }
    }
}
