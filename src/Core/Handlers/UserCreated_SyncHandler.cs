using System;
using System.Threading;
using Core.DataAccess;
using Core.Model;

namespace Core
{
    public class UserCreated_SyncHandler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreated_SyncHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Handle(UserCreated args)
        {
            //Console.WriteLine("Enter - Sync Handler");

            //Console.WriteLine("Sleeping - Sync Handler");
            //Thread.Sleep(3000);
            //Console.WriteLine("Awake - Sync Handler");

            //Console.WriteLine("Commiting UnitOfWork Sync Handler");
            unitOfWork.Commit();
            //Console.WriteLine("Commited UnitOfWork Sync Handler");

            //Console.WriteLine("Exit - Sync Handler");
        }
    }
}