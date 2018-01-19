using System;
using System.Collections.Generic;

namespace Helpers.Core
{
    public static class ServiceEvents
    {
        private static IServiceResolver serviceResolver;
        private static Func<IServiceResolver> resolverFunc;

        public static void Initialize(Func<IServiceResolver> resolverDelegate)
        {
            resolverFunc = resolverDelegate;
        }

        public static void Initialize(IServiceResolver resolver)
        {
            serviceResolver = resolver;
        }


        //Raises the given domain event
        public static void Raise<T>(T args) where T : IServiceEvent
        {
            var resolver = serviceResolver;
            if (resolver == null && resolverFunc != null)
                resolver = resolverFunc.Invoke();

            if (resolver == null)
            {
                throw new Exception("There is no registered ServiceResolver!. Please consider to call ServiceEvents.Initialize(...) at the startup of your application.");
            }
            var handlers = resolver.ResolveAll<IServiceEventHandler<T>>() ?? new List<IServiceEventHandler<T>>();
            foreach (var handler in handlers)
            {
                handler.Handle(args);
            }
        }
    }
}