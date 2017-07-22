using System;

namespace ConsoleApp.Model
{
    internal class ProcessRequest
    {
        public ProcessRequest(Guid id, int stepNumber, string data)
        {
            Id = id;
            StepNumber = stepNumber;
            Data = data;
        }

        public Guid Id { get; }
        public int StepNumber { get; }
        public string Data { get; }
    }
}