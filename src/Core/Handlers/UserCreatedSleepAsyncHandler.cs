using System.Threading.Tasks;
using Core.DataAccess;
using Core.Helpers;
using Core.Model;

namespace Core
{
    public class UserCreatedSleepAsyncHandler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreatedSleepAsyncHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void Handle(UserCreated args)
        {
            Tasker.Run(async () =>
            {
                await Task.Delay(5000);
                unitOfWork.Commit();
            });
        }
    }
}