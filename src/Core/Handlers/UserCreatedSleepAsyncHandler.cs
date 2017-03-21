﻿using System.Threading;
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
            Task.Run(async () =>
            {
                await Task.Delay(9000);
                unitOfWork.Commit();
            });
        }
    }
}