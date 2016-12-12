using System.Threading;
using System.Threading.Tasks;
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



        public void Handle(UserCreated args)
        {

        }


        public async Task HandleAsync(UserCreated args)
        {
            await Task.Run(() =>
                           {
                               Thread.Sleep(5000);
                               unitOfWork.Commit();
                           });
        }
    }
}