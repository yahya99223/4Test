using System;
using Core.Model;


namespace Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void AddUser(User user);
    }
}
