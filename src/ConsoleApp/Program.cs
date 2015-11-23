using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.DomainModel;
using Core.ServicesContracts;
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


            /*var journeyService = serviceResolver.GetService<IJourneyService>();

            journeyService.Start(Guid.Empty);

            var request1 = new JourneyInputRequest()
            {
                StepName = "1_IdentityCard_FrontSide"
            };
            var request2 = new JourneyInputRequest()
            {
                StepName = "2_IdentityCard_BackSide"
            };

            journeyService.Proceed(Guid.Empty, request1);

            journeyService.Proceed(Guid.Empty, request2);

            journeyService.Cancel(Guid.Empty);*/


            Console.ReadLine();
        }
    }
}
