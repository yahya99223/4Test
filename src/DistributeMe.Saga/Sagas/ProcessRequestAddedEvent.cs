using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.Saga
{
    public class ProcessRequestAddedEvent : IProcessRequestAddedEvent
    {
        public ProcessRequestAddedEvent(Guid requestId, byte[] data)
        {
            RequestId = requestId;
            Data = data;
        }

        public Guid RequestId { get; }
        public byte[] Data { get; }
    }
}