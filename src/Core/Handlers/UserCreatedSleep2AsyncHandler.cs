using System.Threading;
using Core.DataAccess;
using Core.Model;


namespace Core
{
    public class UserCreatedSleep2AsyncHandler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreatedSleep2AsyncHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool IsAsync => true;


        public void Handle(UserCreated args)
        {
            Thread.Sleep(5000);
            unitOfWork.Commit();
        }
    }
}