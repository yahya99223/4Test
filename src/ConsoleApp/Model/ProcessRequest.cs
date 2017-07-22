using System;

namespace ConsoleApp.Model
{
    internal class ProcessRequest
    {
        private ProcessRequest(Guid id, int stepNumber, string data)
        {
            Id = id;
            StepNumber = stepNumber;
            Data = data;
        }

        public Guid Id { get; }
        public int StepNumber { get; }
        public string Data { get; }

        public static ProcessRequest Create(int stepNumber, string data)
        {
            return new ProcessRequest(Guid.NewGuid(), stepNumber, data);
        }
    }
}