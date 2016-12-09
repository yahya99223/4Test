using System;
using System.Collections.Generic;

namespace Core
{
    public interface IServiceResolver
    {
        void Initialize(string rootFolder);
        void Stop();
        T Resolve<T>();
        IList<T> ResolveAll<T>();
        IDisposable BeginScope();
    }
}