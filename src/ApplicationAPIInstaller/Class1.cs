using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace IDScan.OnboardingSuite.Shared.WindsorInstallers.ApplicationAPIInstaller
{
    public class Class1Scope : IScopeAccessor
    {
        private static readonly ConcurrentDictionary<Guid, ILifetimeScope> collection = new ConcurrentDictionary<Guid, ILifetimeScope>();


        public ILifetimeScope GetScope(CreationContext context)
        {

            return collection.GetOrAdd(Guid.NewGuid(), id => new CallContextLifetimeScope());
        }

        public void Dispose()
        {
            foreach (var scope in collection)
            {
                scope.Value.Dispose();
            }
            collection.Clear();
        }
    }
}