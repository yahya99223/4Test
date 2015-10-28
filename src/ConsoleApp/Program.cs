using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using IoC;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceResolver = new ServiceResolver();
            serviceResolver.Initialize(AppDomain.CurrentDomain.BaseDirectory);

            var userService = serviceResolver.GetService<IUserService>();
            userService.Add("Wahid");
            userService.ChangeStatus(1, false);
            userService.ChangeStatus(1, true);

            DomainEvents.ClearCallbacks();
            Console.ReadLine();
        }
    }
}
