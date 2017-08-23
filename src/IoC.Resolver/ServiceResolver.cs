using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDScan.SaaS.SharedBlocks.Helpers.Core;
using Microsoft.Practices.Unity;

namespace IoC.Resolver
{
    public class ServiceResolver : IServiceResolver
    {
        public ServiceResolver()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IServiceResolver>(this,new ContainerControlledLifetimeManager());

        }
        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }

        public IList<T> ResolveAll<T>()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
