using Core.DomainModel.User;

namespace Core.ServicesContracts
{
    public interface IUserService
    {
        User Add(string userName);
        User ChangeStatus(int userId, bool isActive);
    }
}