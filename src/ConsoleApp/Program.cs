using System;
using DomainModel;

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

                while (captureSession.State != CaptureSessionState.Finished)
                {
                    Console.WriteLine();
                    Console.WriteLine($" Please enter the data to process it. The string length should be more than 3 characters");
                    Console.WriteLine();
                    Console.WriteLine();
                    
                    captureSession.AddProcessRequest(Console.ReadLine());
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
