using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;
using Core.DomainModel;
using Core.Modularity;

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
                return container.Resolve<T>(typeof (T).Name + Statics.CurrentBusinessCode);
            }
            catch (ComponentNotFoundException ex)
            {
                return container.Resolve<T>();
            }
        }


        public IList<T> GetAllService<T>()
        {
            var result =  new List<T>();            
            var temp = container.ResolveAll<T>();
            if (temp != null)
            {
                result = temp.ToList();
                foreach (T item in temp.ToList())
                {
                    var attribute = item.GetType().GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof (ReplaceAttribute));
                    if (attribute != null)
                    {
                        var replacedItems = result.Where(x => !x.Equals(item) && x.GetType().Name == item.GetType().Name);
                        foreach (var replaced in replacedItems.ToList())
                        {
                            result.Remove(replaced);
                        }
                    }
                }
            }
            return result;
        }
    }
}
