using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;
using Core.DomainModel;

namespace IoC
{
    public class ServiceResolver : IServiceResolver
    {
        private static WindsorContainer container;


        public void Initialize(string folder)
        {
            container = new WindsorContainer();
            container.Register(Component.For<IServiceResolver>().ImplementedBy<ServiceResolver>().LifestyleSingleton());        
            container.Install(FromAssembly.InDirectory(new AssemblyFilter(Path.Combine(folder, "Customized"), "*Installer.dll")));
            container.Install(FromAssembly.InDirectory(new AssemblyFilter(Path.Combine(folder, "Default"), "*Installer.dll")));
            DomainEvents.Initialize(this);
        }


        public T GetService<T>()
        {
            try
            {
                return container.Resolve<T>("FakeCustomer");
            }
            catch (ComponentNotFoundException ex)
            {
                return container.Resolve<T>();
            }
        }


        public IList<T> GetAllService<T>()
        {
            var result =  container.ResolveAll<T>();
            if (result != null)
                return result.ToList();
            return new List<T>();
        }
    }
}
