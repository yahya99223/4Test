using System.Collections.Generic;

namespace Core
{
    public interface IServiceResolver
    {
        IList<T> GetAllService<T>();
        T GetService<T>();
        void Initialize(string folder);
    }
}