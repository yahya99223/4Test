using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;


        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public void Add(string userName)
        {
            var maxId = userRepository.GetMaxId();
            var user = new User(maxId + 1, userName, true);
            var addedUser = new UserAdded(user);
            DomainEvents.Register<UserAdded>(x => addingUser(addedUser));
            userRepository.Add(user);
            DomainEvents.Raise(addedUser);
        }


        private void addingUser(UserAdded userAdded)
        {
            Console.WriteLine("Adding {0}. O_O ", userAdded.User.UserName);
        }


        public void ChangeStatus(int userId, bool isActive)
        {
            var user = userRepository.GetById(userId);
            if (user == null)
                throw new NullReferenceException();
            user.ChangeStatus(isActive);

            userRepository.Save(user);
        }
    }
}