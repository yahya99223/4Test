using System.Threading.Tasks;
using Core.DataAccess;
using Core.Helpers;
using Core.Model;

namespace Core
{
    public class UserCreatedSleepAsync2Handler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreatedSleepAsync2Handler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void Handle(UserCreated args)
        {
            Tasker.Run(async () =>
            {
                await Task.Delay(9000);
                unitOfWork.Commit();
            });
        }
    }
}