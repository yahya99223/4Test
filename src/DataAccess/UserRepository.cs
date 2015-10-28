using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IList<User> usersList;


        public UserRepository()
        {
            this.usersList = new List<User>();
        }


        public int GetMaxId()
        {
            return usersList.Max(x => (int?) x.Id) ?? 0;
        }


        public User GetById(int userId)
        {
            return usersList.FirstOrDefault(u => u.Id == userId);
        }


        public void Add(User user)
        {
            if (usersList.Any(u => u.Id == user.Id))
                throw new DuplicateNameException();

            usersList.Add(user);
        }


        public void Save(User user)
        {
            var oldUser = usersList.FirstOrDefault(u => u.Id == user.Id);
            if (oldUser == null)
                throw new InstanceNotFoundException();

            usersList.Remove(oldUser);
            usersList.Add(user);
        }
    }
}