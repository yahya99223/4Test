using System.Collections.Generic;

namespace DomainModel
{
    public interface IServiceResolver
    {
        IList<T> GetAllService<T>();
        T GetService<T>();
        void Initialize(string folder);
    }
}