﻿using System;
using Core;
using Core.DataAccessContracts;
using Core.DomainModel;
using Core.DomainModel.User;
using Core.ServicesContracts;

namespace Services.FakeCustomer.User
{
    public class UserService : Default.User.UserService
    {
        private readonly IServiceResolver serviceResolver;


        public UserService(IUserRepository userRepository,IServiceResolver serviceResolver) : base(userRepository,serviceResolver)
        {
            this.serviceResolver = serviceResolver;
        }


        public override Core.DomainModel.User.User ChangeStatus(int userId, bool isActive)
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
            return user;
        }

        private void activatingUser(UserBecameActive userBecameActive)
        {
            Console.WriteLine("Fake -> Welcome back {0}. this is your fake service ", userBecameActive.User.UserName);
        }
    }
}