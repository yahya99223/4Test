using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;


namespace Core
{
    public static class DomainEvents
    {
        private static IServiceResolver serviceResolver;


        public static void Initialize(IServiceResolver resolver)
        {
            serviceResolver = resolver;
        }


        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> actions;


        //Registers a callback for the given domain event
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }


        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks()
        {
            actions = null;
        }


        //Raises the given domain event
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (serviceResolver != null)
            {
                var handlers = serviceResolver.ResolveAll<IHandles<T>>() ?? new List<IHandles<T>>();
                foreach (var handler in handlers)
                {
                    handler.Handle(args);
                }
                try
                {
                    IDisposable backgroundScope = null;
                    Task.Run(() =>
                    {
                        backgroundScope = serviceResolver.StartScope();
                        var backgroundHandlers = serviceResolver.ResolveAll<IBackgroundHandles<T>>() ?? new List<IBackgroundHandles<T>>();
                        var tasks = backgroundHandlers.Select(x => x.HandlerTask(args)).ToArray();
                        Task.WaitAll(tasks);
                    }).ContinueWith(oldTask =>
                    {
                        backgroundScope.Dispose();
                    });
                }
                catch (Exception e)
                {
                    StaticInfo.Exception = e.Message;
                }
            }
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action is Action<T>)
                        ((Action<T>) action)(args);
                }
            }
        }
    }
}