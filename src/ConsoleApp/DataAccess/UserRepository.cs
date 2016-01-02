using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using Infrastructure;

namespace ConsoleApp.DataAccess
{
    public class UserRepository
    {
        private static IList<User> inMemoryList = new EnlistmentNotificationList<User>();


        public UserRepository()
        {
        }


        public IList<User> GetAll()
        {
            return inMemoryList;
        }


        public User GetById(int id)
        {
            return inMemoryList.FirstOrDefault(x => x.Id == id);
        }


        public void Add(User user)
        {
            if (inMemoryList.Any(u => u.Id == user.Id || u.Name == user.Name))
                throw new Exception("Duplicated");
            inMemoryList.Add(user);
        }


        public void Update(User user)
        {
            var oldItem = GetById(user.Id);
            if (oldItem != null)
            {
                inMemoryList.Remove(oldItem);
                Add(user);
            }
            else
                throw new Exception("Not Found");
        }


        public void Delete(int id)
        {
            var oldItem = GetById(id);
            if (oldItem != null)
                inMemoryList.Remove(oldItem);
            else
                throw new Exception("Not Found");
        }
    }
}