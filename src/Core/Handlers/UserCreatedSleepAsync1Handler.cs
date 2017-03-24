using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Helpers;
using Core.Model;

namespace Core
{
    public class UserCreatedSleepAsync1Handler : IHandles<UserCreated>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserCreatedSleepAsync1Handler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void Handle(UserCreated args)
        {
            Console.WriteLine("Enter - Async1 Handler");
            Tasker.Run(() =>
            {
                Console.WriteLine("Sleeping - Async1 Handler");
                Tasker.Run(Task.Delay(5000));
                Console.WriteLine("Awake - Async1 Handler");

            Console.WriteLine("Commiting UnitOfWork Async1 Handler");
                unitOfWork.Commit();
            Console.WriteLine("Commited UnitOfWork Async1 Handler");
            });
            Console.WriteLine("Exit - Async1 Handler");
        }
    }
}