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
            try
            {
                Console.WriteLine($"Please enter the maximum number of pages that your capture session may have");
                var captureSession = DocumentCaptureSession.Create(int.Parse(Console.ReadLine()));

                Console.WriteLine($"-> CaptureSession {captureSession.Id} is {captureSession.State}");
                Console.WriteLine("====================***********====================");

                while (captureSession.State != CaptureSessionState.Finished||true)
                {
                    Console.WriteLine();
                    Console.WriteLine($" Please enter the data to process it. The string length should be more than 3 characters");
                    Console.WriteLine();
                    Console.WriteLine();

                    var request = ProcessRequest.Create(Console.ReadLine());
                    captureSession.AddProcessRequest(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("====================EXCEPTION====================");
                Console.WriteLine(e);
                Console.WriteLine("============================================================");
            }
            Console.ReadKey();
        }
    }
}
