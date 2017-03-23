using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
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
            Task.Run(async () =>
                {
                    var callContextId = (Guid) CallContext.LogicalGetData("CallContextId");
                    await Tasker.Run(callContextId, async () =>
                    {
                        await Task.Delay(9000);
                        unitOfWork.Commit();
                    });
                }
            );
        }
    }
}