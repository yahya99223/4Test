using System.Threading;
using Core.DataAccess;
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

        public bool IsAsync => true;


        public void Handle(UserCreated args)
        {
            Thread.Sleep(7000);
            unitOfWork.Commit();
        }
    }
}