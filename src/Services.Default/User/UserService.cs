﻿using System;
using Core;
using Core.DataAccessContracts;
using Core.DomainModel;
using Core.DomainModel.User;
using Core.ServicesContracts;

namespace Services.Default.User
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository userRepository;
        private readonly IServiceResolver serviceResolver;


        public UserService(IUserRepository userRepository,IServiceResolver serviceResolver)
        {
            this.userRepository = userRepository;
            this.serviceResolver = serviceResolver;
        }


        public virtual void Add(string userName)
        {
            var maxId = userRepository.GetMaxId();
            var user = new Core.DomainModel.User.User(maxId + 1, userName, true);
            var addedUser = new AddedModel<Core.DomainModel.User.User>(user);
            DomainEvents.Register<AddedModel<Core.DomainModel.User.User>>(x => addingUser(addedUser));
            var validationEngine = new ValidationEngine(serviceResolver);
            var validationResult = validationEngine.Validate(user);
            foreach (var validation in validationResult)
            {
                Console.WriteLine(validation);
            }
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

            if (isActive)
            {
                var userBecameActive = new UserBecameActive(user);
                DomainEvents.Register<UserBecameActive>(x => activatingUser(userBecameActive));
            }

            user.ChangeStatus(isActive);

            userRepository.Save(user);
        }

        private void activatingUser(UserBecameActive userBecameActive)
        {
            Console.WriteLine("Welcome back {0}. this is your Default service ", userBecameActive.User.UserName);
        }
    }
}