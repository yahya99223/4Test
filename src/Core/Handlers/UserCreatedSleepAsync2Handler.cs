using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
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
            var callContextId = (Guid)CallContext.LogicalGetData("CallContextId");

            Tasker.Run(callContextId, async () =>
            {
                await Task.Delay(5000);
                unitOfWork.Commit();
            });
        }
    }
}