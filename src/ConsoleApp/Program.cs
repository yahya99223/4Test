using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.DataAccess;
using ConsoleApp.Model;
using ConsoleApp.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var doctorService = new DoctorService(false);
                var userService = new UserService(false);

                userService.Add(new User(1, "First"));
                doctorService.Add(new Doctor(1, "First"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
