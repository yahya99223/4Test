using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class User
    {
        private bool isActive;
        
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                var x = isActive;
                isActive = value;
                DomainEvents.Raise(new UserStatusChanged(this, x));
            }
        }

        public static User Create(string name)
        {
            var user = new User()
            {
                Id = 1,
                Name = name,
                IsActive = true,
            };
            DomainEvents.Raise(new UserCreated(user));
            return user;
        }
    }
}
