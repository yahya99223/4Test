using System.Threading;
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

        public bool IsAsync
        {
            get { return false; }
        }

        public void Handle(UserCreated args)
        {
            //Thread.Sleep(1000);
            unitOfWork.Commit();
        }
    }
}