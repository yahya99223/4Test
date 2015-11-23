using System;
using Core.DataAccessContracts;
using Core.DomainModel;
using Core.ServicesContracts;

namespace Services.Default.User
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository userRepository;


        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public virtual void Add(string userName)
        {
            var maxId = userRepository.GetMaxId();
            var user = new Core.DomainModel.User.User(maxId + 1, userName, true);
            var addedUser = new AddedModel<Core.DomainModel.User.User>(user);
            DomainEvents.Register<AddedModel<Core.DomainModel.User.User>>(x => addingUser(addedUser));
            userRepository.Add(user);
            DomainEvents.Raise(addedUser);
        }


        private void addingUser(AddedModel<Core.DomainModel.User.User> userAdded)
        {
            Console.WriteLine("Adding {0}. O_O ", userAdded.Model.UserName);
        }


        public virtual void ChangeStatus(int userId, bool isActive)
        {
            var user = userRepository.GetById(userId);
            if (user == null)
                throw new NullReferenceException();
            user.ChangeStatus(isActive);

            userRepository.Save(user);
        }
    }
}