using System;
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


        /*public void Handle(UserCreated args)
        {
            Console.WriteLine("Enter - Async2 Handler");
            Tasker.Run(() =>
            {
                Console.WriteLine("Sleeping - Async2 Handler");
                Tasker.Run(Task.Delay(9000));
                Console.WriteLine("Awake - Async2 Handler");

                Console.WriteLine("Commiting UnitOfWork Async2 Handler");
                unitOfWork.Commit();
                Console.WriteLine("Commited UnitOfWork Async2 Handler");
            });
            Console.WriteLine("Exit - Async2 Handler");
        }*/

        public void Handle(UserCreated args)
        {
            Console.WriteLine("Enter - Async2 Handler");
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Sleeping - Async2 Handler");
                Thread.Sleep(9000);
                Console.WriteLine("Awake - Async2 Handler");

                Console.WriteLine("Commiting UnitOfWork Async2 Handler");
                unitOfWork.Commit();
                Console.WriteLine("Commited UnitOfWork Async2 Handler");
            }, TaskCreationOptions.AttachedToParent);
            Console.WriteLine("Exit - Async2 Handler");
        }
    }
}