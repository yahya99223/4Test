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
                string passedValue = null;
                while (passedValue == null || passedValue.ToLower() != "exit")
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Please enter the data to process it. The string length should be more than 3 characters");
                        Console.WriteLine($"--> Write 'exit' to shut down");
                        Console.WriteLine($"--> Or 'rest n' to recreate the CaptureSession with n as maximum number of pages ");
                        Console.WriteLine();
                        Console.WriteLine();
                        passedValue = Console.ReadLine();
                        if (passedValue?.ToLower() == "exit")
                            continue;

                        if (passedValue != null && passedValue.ToLower().StartsWith("rest "))
                            captureSession = DocumentCaptureSession.Create(int.Parse(passedValue.Split(' ')[1]));
                        else
                            captureSession.AddProcessRequest(passedValue);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("====================EXCEPTION====================");
                        Console.WriteLine(e);
                        Console.WriteLine("============================================================");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("====================EXCEPTION====================");
                Console.WriteLine(e);
                Console.WriteLine("============================================================");
            }
        }
    }
}
