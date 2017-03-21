using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;

namespace IoC
{
    public class ServiceResolver : IServiceResolver
    {
        public static WindsorContainer Container;


        public ServiceResolver(string rootFolder)
        {
            if (string.IsNullOrEmpty(rootFolder) || !Directory.Exists(rootFolder))
                throw new DirectoryNotFoundException();

            Container = new WindsorContainer();

            Container.Register(Component.For<IServiceResolver>().Instance(this).LifestyleSingleton());
            Container.Register(Component.For<IWindsorContainer>().Instance(Container).LifestyleSingleton());


            Container.Install(FromAssembly.InDirectory(new AssemblyFilter(rootFolder, "*Installer.dll")));

            DomainEvents.Initialize(this);
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public IList<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>().ToList();
        }

        public IDisposable StartScope()
        {
            return Container.BeginScope();
        }
    }
}