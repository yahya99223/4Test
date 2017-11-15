using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostPattern = "http://+:" + 8088;

            var webApplication = WebApp.Start<Startup>(hostPattern);
            Console.ReadLine();
            webApplication.Dispose();
        }
    }
}
