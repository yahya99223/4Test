namespace DomainModel
{
    public interface IUserRepository
    {
        int GetMaxId();
        User GetById(int userId);
        void Add(User user);
        void Save(User user);
    }
}