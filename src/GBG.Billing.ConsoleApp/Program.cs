using System;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Events;

namespace GBG.Billing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Billing Console";

            try
            {
                Console.WriteLine("Press any key to stop!");
                Console.ReadKey();
            }
            finally
            {
            }
        }
    }
}
