namespace Core.Model
{
    public class UserCreated : IDomainEvent
    {
        public User User { get; set; }

        public UserCreated(User user)
        {
            User = user;
        }
    }


    public class UserStatusChanged : IDomainEvent
    {
        public User User { get; set; }
        public bool OldStatus { get; set; }

        public UserStatusChanged(User user, bool oldStatus)
        {
            User = user;
            OldStatus = oldStatus;
        }
    }
}