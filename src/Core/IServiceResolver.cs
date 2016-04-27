using System;
using System.Collections.Generic;

namespace Core
{
    public interface IServiceResolver
    {
        void Initialize(string rootFolder);
        void Stop();
        T Resolve<T>();
        object Resolve(Type type);
        IList<T> ResolveAll<T>();
    }
}