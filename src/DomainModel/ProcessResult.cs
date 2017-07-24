using System;

namespace DomainModel
{
    public abstract class ProcessResult
    {
        protected ProcessResult(Guid id, Guid requestId, int stepNumber)
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