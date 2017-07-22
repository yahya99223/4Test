using System;

namespace ConsoleApp.Model
{
    internal abstract class ProcessResult
    {
        public ProcessResult(Guid id, Guid requestId, int stepNumber)
        {
            Id = id;
            RequestId = requestId;
            StepNumber = stepNumber;
        }

        public Guid Id { get; }
        public Guid RequestId { get; }
        public int StepNumber { get; }
    }
}