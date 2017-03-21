using System.Threading.Tasks;
using Core.DataAccess;
using Core.Model;

namespace Core
{
    public class UserCreatedSleepSyncHandler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreatedSleepSyncHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Handle(UserCreated args)
        {
            unitOfWork.Commit();
        }
    }
}