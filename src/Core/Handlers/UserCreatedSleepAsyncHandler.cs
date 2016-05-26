using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool IsAsync
        {
            get { return true; }
        }

        public void Handle(UserCreated args)
        {
            Thread.Sleep(9000);
            unitOfWork.Commit();
        }
    }


    public class UserCreatedSleep2AsyncHandler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreatedSleep2AsyncHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool IsAsync
        {
            get { return true; }
        }

        public void Handle(UserCreated args)
        {
            Thread.Sleep(13000);
            unitOfWork.Commit();
        }
    }
}