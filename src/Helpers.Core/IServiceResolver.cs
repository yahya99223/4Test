using System;
using System.Collections.Generic;

namespace Helpers.Core
{
    public interface IServiceResolver : IDisposable
    {
        IDisposable RequireScope();
        T Resolve<T>();
        IList<T> ResolveAll<T>();
    }
}