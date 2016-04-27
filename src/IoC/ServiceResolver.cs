using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;

namespace IoC
{
    public class ServiceResolver : IServiceResolver
    {
        public static WindsorContainer Container;


        public void Initialize(string rootFolder)
        {
            //LogFactory.InitializeWith<Log4NetLogger>();

            //ToDo Add the message for not founded root folder
            if (string.IsNullOrEmpty(rootFolder) || !Directory.Exists(rootFolder))
                throw new DirectoryNotFoundException();

            Container = new WindsorContainer();

            Container.Register(Component.For<IWindsorContainer>().Instance(Container).LifestyleSingleton());

            var installFolder = Path.Combine(rootFolder, "Install");
            if (!Directory.Exists(installFolder))
                installFolder = rootFolder;

            Container.Install(FromAssembly.InDirectory(new AssemblyFilter(installFolder, "*Installer.dll")));
        }


        public void Stop()
        {
            Container.Dispose();
        }


        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public IList<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>().ToList();
        }
    }
}