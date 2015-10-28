namespace Core
{
    public class UserBecameInactive : IDomainEvent
    {
        public UserBecameInactive(User user)
        {
            User = user;
        }


        public User User { get; set; }
    }
}