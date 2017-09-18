using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessRequestFinishedEvent
    {
        Guid RequestId { get; }
        byte[] Data { get; }
    }

    public class ProcessRequestFinishedEvent : IProcessRequestFinishedEvent
    {
        public ProcessRequestFinishedEvent(Guid requestId, byte[] data)
        {
            RequestId = requestId;
            Data = data;
        }

        public Guid RequestId { get; }
        public byte[] Data { get; }
    }
}