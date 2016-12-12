using System.Threading;
using System.Threading.Tasks;
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


        public void Handle(UserCreated args)
        {
        }


        public async Task HandleAsync(UserCreated args)
        {
            await Task.Run(() =>
                           {
                               Thread.Sleep(7000);
                               unitOfWork.Commit();
                           });
        }
    }
}