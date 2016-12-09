using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;
using Core;


namespace IDScan.OnboardingSuite.Shared.WindsorInstallers.ApplicationAPIInstaller
{
    public class WindsorHttpDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;
        private readonly IServiceResolver serviceResolver;


        public WindsorHttpDependencyResolver(IWindsorContainer container,IServiceResolver serviceResolver)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            this.serviceResolver = serviceResolver;
        }

        public object GetService(Type t)
        {
            return this.container.Kernel.HasComponent(t)
             ? this.container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return this.container.ResolveAll(t).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this.container, serviceResolver.MiddlewareScope);
        }

        public void Dispose()
        {
        }
    }
}