using System;
using System.Collections.Generic;


namespace Core
{
    public interface IServiceResolver : IDisposable
    {
        T Resolve<T>();
        IList<T> ResolveAll<T>();
        IDisposable SetMiddlewareScope();
        IDisposable MiddlewareScope { get; }
    }

    
}