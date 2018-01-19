using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers.Core
{
    public static class DomainEvents
    {
        private static IServiceResolver serviceResolver;
        private static Func<IServiceResolver> resolverFunc;
        private static readonly ConcurrentDictionary<string, ConcurrentQueue<Delegate>> actionsDictionary = new ConcurrentDictionary<string, ConcurrentQueue<Delegate>>();
        private static readonly ConcurrentDictionary<string, ConcurrentBag<Delegate>> stickyActionsDictionary = new ConcurrentDictionary<string, ConcurrentBag<Delegate>>();

        public static void Initialize(Func<IServiceResolver> resolverDelegate)
        {
            resolverFunc = resolverDelegate;
        }

        public static void Initialize(IServiceResolver resolver)
        {
            serviceResolver = resolver;
        }



        //Registers a callback for the given domain event
        public static void Register<T>(Guid eventKey, Action<T> callback, bool autoCleanup = true) where T : IDomainEvent
        {
            if (autoCleanup)
            {
                actionsDictionary.AddOrUpdate(eventKey + typeof(T).FullName, x => new ConcurrentQueue<Delegate>(new Delegate[] {callback}), (key, list) =>
                {
                    list.Enqueue(callback);
                    return list;
                });
            }
            else
            {
                stickyActionsDictionary.AddOrUpdate(eventKey + typeof(T).FullName, x => new ConcurrentBag<Delegate>(new Delegate[] {callback}), (key, list) =>
                {
                    list.Add(callback);
                    return list;
                });
            }
        }


        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks(Guid eventId, bool clearstickyActions = true)
        {
            var keys = actionsDictionary.Keys.Where(k => k.StartsWith(eventId.ToString())).ToList();
            foreach (var key in keys)
            {
                ConcurrentQueue<Delegate> removedActions;
                actionsDictionary.TryRemove(key, out removedActions);
            }

            if (clearstickyActions)
            {
                var stickykeys = stickyActionsDictionary.Keys.Where(k => k.StartsWith(eventId.ToString())).ToList();
                foreach (var key in stickykeys)
                {
                    ConcurrentBag<Delegate> removedActions;
                    stickyActionsDictionary.TryRemove(key, out removedActions);
                }
            }
        }

        public static void RaiseAsync<T>(T args) where T : IDomainEvent
        {
            Task.Run(() =>
            {
                var taskArgs = args;
                Raise(taskArgs);
            });
        }

        //Raises the given domain event
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            var fullEventKey = args.EventKey + typeof(T).FullName;
            
            ConcurrentQueue<Delegate> actionsList;
            if (actionsDictionary.TryGetValue(fullEventKey, out actionsList) && actionsList != null && actionsList.Any())
            {
                Delegate action;
                while (actionsList.TryDequeue(out action))
                {
                    if (action is Action<T>)
                        ((Action<T>) action)(args);
                }
            }

            ConcurrentBag<Delegate> stickyActionsList;
            if (stickyActionsDictionary.TryGetValue(fullEventKey, out stickyActionsList) && stickyActionsList != null)
            {
                foreach (var action in stickyActionsList)
                {
                    if (action is Action<T>)
                        ((Action<T>) action)(args);
                }
            }
            resolveHandlers(args);
        }

        private static void resolveHandlers<T>(T args) where T : IDomainEvent
        {
            var resolver = serviceResolver;
            if (resolver == null && resolverFunc != null)
                resolver = resolverFunc.Invoke();

            if (resolver == null)
            {
                throw new Exception("There is no registered ServiceResolver!. Please consider to call DomainEvents.Initialize(...) at the startup of your application.");
            }

            var handlers = resolver.ResolveAll<IHandles<T>>() ?? new List<IHandles<T>>();
            if (handlers.Any())
            {
                foreach (var handler in handlers)
                {
                    handler.Handle(args);
                }
            }
            else
            {
            }
        }
    }
}