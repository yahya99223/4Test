using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Helpers;
using Core.Model;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(User user)
        {
            unitOfWork.AddUser(user);
        }

        public async Task AddAsync(User user)
        {
            await Task.Run(() => {
                unitOfWork.AddUser(user);
            });
        }
    }
}