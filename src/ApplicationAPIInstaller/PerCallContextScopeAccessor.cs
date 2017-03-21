using System;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace IDScan.OnboardingSuite.Shared.WindsorInstallers.ApplicationAPIInstaller
{
    public class PerCallContextScopeAccessor : IScopeAccessor
    {
        private static readonly ConcurrentDictionary<Guid, ILifetimeScope> collection = new ConcurrentDictionary<Guid, ILifetimeScope>();

        public ILifetimeScope GetScope(Castle.MicroKernel.Context.CreationContext context)
        {
            var callContextId = (Guid) CallContext.LogicalGetData("CallContextId");
            return collection.GetOrAdd(callContextId, id => new ThreadSafeDefaultLifetimeScope());
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