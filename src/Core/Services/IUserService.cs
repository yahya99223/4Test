using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Model;

namespace Core.Services
{
    public interface IUserService
    {
        void Add(User user);
    }


    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(User user)
        {
            StaticInfo.Users += 1;
        }
    }
}