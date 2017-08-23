using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Core;
using IDScan.SaaS.SharedBlocks.Helpers.Core;
using IoC.Installer;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using WebApp;
using WebApp.Middlewares;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
            });

            //IContainer container = new ContainerBuilder().Build();
            var builder = new ContainerBuilder();
            builder.RegisterType<ServiceResolver>().As<IServiceResolver>().SingleInstance();
            //builder.RegisterType<SetLifetimeScopeMiddleware>().InstancePerLifetimeScope();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new HiModule());

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            DomainEvents.Initialize(() =>
            {
                return container.Resolve<IServiceResolver>();
            });

            app.Use<StatisticsMiddleware>();
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);


            app.UseCors(CorsOptions.AllowAll);


            app.UseWebApi(GlobalConfiguration.Configuration);
        }
    }

    public class ServiceResolver : IServiceResolver
    {
        private readonly IComponentContext context;

        public ServiceResolver(IComponentContext context)
        {
            this.context = context;
            Statistics.ServiceResolverCount += 1;
        }

        public T Resolve<T>()
        {
            return context.Resolve<T>();
        }

        public IList<T> ResolveAll<T>()
        {
            return context.Resolve<IEnumerable<T>>().NullableToList();
        }

        public void Dispose()
        {
            
        }
    }
    /*
    public class ServiceResolver : IServiceResolver
    {
        private readonly IContainer container;

        public ServiceResolver(IContainer container)
        {
            this.container = container;
            Statistics.ServiceResolverCount += 1;
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public IList<T> ResolveAll<T>()
        {
            return container.Resolve<IEnumerable<T>>().NullableToList();
            //return GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope().Resolve<IEnumerable<T>>().NullableToList();
        }

        public void Dispose()
        {
            GlobalConfiguration.Configuration.DependencyResolver.Dispose();
        }
    }*/
    /*public class ServiceResolver : IServiceResolver
    {
        public ServiceResolver()
        {
            Statistics.ServiceResolverCount += 1;
        }

        public T Resolve<T>()
        {
            return (T) GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(T));
        }

        public IList<T> ResolveAll<T>()
        {
            var result = GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(T));
            return result.Cast<T>().NullableToList();
            //return GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope().Resolve<IEnumerable<T>>().NullableToList();
        }

        public void Dispose()
        {
            GlobalConfiguration.Configuration.DependencyResolver.Dispose();
        }
    }*/
}