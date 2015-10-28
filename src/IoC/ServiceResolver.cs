using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DomainModel;

namespace IoC
{
    public class ServiceResolver : IServiceResolver
    {
        private WindsorContainer container;


        public void Initialize(string folder)
        {
            container = new WindsorContainer();
            container.Install(FromAssembly.InDirectory(new AssemblyFilter(folder, "*.dll")));
            DomainEvents.Initialize(this);
        }


        public T GetService<T>()
        {
            return container.Resolve<T>();
        }


        public IList<T> GetAllService<T>()
        {
            return container.ResolveAll<T>().ToList();
        }
    }
}
