using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var page = Page.Create(ProcessRequest.Create(1, "we"));
            Console.WriteLine($"The page state is: {page.State}");
            Console.WriteLine($"The page Result is: {page.Result}");
            Console.WriteLine($"================================");

            page.AddProcessRequest(ProcessRequest.Create(1, "ab"));
            //page.AddProcessRequest(ProcessRequest.Create(1, "cd"));
            //page.AddProcessRequest(ProcessRequest.Create(1, "Wahid"));

            Console.ReadKey();
        }
    }
}
