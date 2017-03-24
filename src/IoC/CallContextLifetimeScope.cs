﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle.Scoped;
using Castle.Windsor;

namespace IoC
{
    /// <summary>
    ///     Provides explicit lifetime scoping within logical path of execution. Used for types with
    ///     <see
    ///         cref="LifestyleType.Scoped" />
    ///     .
    /// </summary>
    /// <remarks>
    ///     The scope is passed on to child threads, including ThreadPool threads. The capability is limited to single
    ///     <see cref="AppDomain" />
    ///     and should be used cautiously as call to <see cref="Dispose" /> may occur while the child thread is still
    ///     executing, what in turn may lead to subtle threading bugs.
    /// </remarks>
    public class CallContextLifetimeScope : ILifetimeScope
    {
        private static readonly ConcurrentDictionary<Guid, CallContextLifetimeScope> appDomainLocalInstanceCache =
            new ConcurrentDictionary<Guid, CallContextLifetimeScope>();

        private static readonly string keyInCallContext = "castle.lifetime-scope-" + AppDomain.CurrentDomain.Id.ToString(CultureInfo.InvariantCulture);
        private readonly Guid contextId;
        private readonly Lock @lock = Lock.Create();
        private readonly CallContextLifetimeScope parentScope;
        private ScopeCache cache = new ScopeCache();

        public CallContextLifetimeScope(IKernel container) : this()
        {
        }

        public CallContextLifetimeScope()
        {
            var parent = ObtainCurrentScope();
            if (parent != null)
                parentScope = parent;
            contextId = Guid.NewGuid();
            var added = appDomainLocalInstanceCache.TryAdd(contextId, this);
            Debug.Assert(added);
            SetCurrentScope(this);
        }

        public CallContextLifetimeScope(IWindsorContainer container) : this()
        {
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            using (var token = @lock.ForReadingUpgradeable())
            {
                if (cache == null)
                    return;
                token.Upgrade();
                cache.Dispose();
                cache = null;

                if (parentScope != null)
                    SetCurrentScope(parentScope);
                else
                    CallContext.FreeNamedDataSlot(keyInCallContext);
            }
            CallContextLifetimeScope @this;
            appDomainLocalInstanceCache.TryRemove(contextId, out @this);
        }

        public Burden GetCachedInstance(ComponentModel model, ScopedInstanceActivationCallback createInstance)
        {
            using (var token = @lock.ForReadingUpgradeable())
            {
                var burden = cache[model];
                if (burden == null)
                {
                    token.Upgrade();

                    burden = createInstance(delegate { });
                    cache[model] = burden;
                }
                return burden;
            }
        }

        [SecuritySafeCritical]
        private void SetCurrentScope(CallContextLifetimeScope lifetimeScope)
        {
            CallContext.LogicalSetData(keyInCallContext, lifetimeScope.contextId);
        }

        [SecuritySafeCritical]
        public static CallContextLifetimeScope ObtainCurrentScope()
        {
            var scopeKey = CallContext.LogicalGetData(keyInCallContext);
            if (scopeKey == null)
                return null;
            CallContextLifetimeScope scope;
            appDomainLocalInstanceCache.TryGetValue((Guid) scopeKey, out scope);
            return scope;
        }
    }
}