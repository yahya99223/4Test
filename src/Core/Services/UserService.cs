using Core.DataAccess;
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
            StaticInfo.Users += 1;
        }
    }
}