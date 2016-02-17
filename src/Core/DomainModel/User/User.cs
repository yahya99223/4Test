namespace Core.DomainModel.User
{
    public class User
    {
        public User(int id, string userName, bool isActive)
        {
            Id = id;
            UserName = userName;
            IsActive = isActive;
        }


        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; set; }
        public bool IsActive { get; private set; }


        public void ChangeStatus(bool isActive)
        {
            IsActive = isActive;
            if (isActive)
                DomainEvents.Raise(new UserBecameActive(this));
            else
                DomainEvents.Raise(new UserBecameInactive(this));
        }
    }
}