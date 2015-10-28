﻿namespace Core
{
    public class UserBecameActive : IDomainEvent
    {
        public UserBecameActive(User user)
        {
            User = user;
        }


        public User User { get; set; }
    }
}