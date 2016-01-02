using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.DataAccess;
using ConsoleApp.Model;
using Infrastructure;

namespace ConsoleApp.Services
{
    public class UserService
    {
        private readonly bool isSubService;
        private readonly UnitOfWork unitOfWork;

        public UserService(bool isSubService, UnitOfWork unitOfWork = null)
        {
            this.isSubService = isSubService;
            this.unitOfWork = unitOfWork ?? new UnitOfWork();
        }


        public IList<User> GetAll()
        {
            return unitOfWork.UserRepository.GetAll();
        }


        public User GetById(int id)
        {
            return unitOfWork.UserRepository.GetById(id);
        }


        public void Add(User user)
        {
            unitOfWork.UserRepository.Add(user);
            if (!isSubService)
                unitOfWork.Commit();
        }


        public void Update(User user)
        {
            unitOfWork.UserRepository.Update(user);
            if (!isSubService)
                unitOfWork.Commit();
        }


        public void Delete(int id)
        {
            unitOfWork.UserRepository.Delete(id);
            if (!isSubService)
                unitOfWork.Commit();
        }
    }
}