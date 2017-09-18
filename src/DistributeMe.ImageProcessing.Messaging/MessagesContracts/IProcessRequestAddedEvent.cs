using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessRequestAddedEvent
    {
        Guid RequestId { get; }
        byte[] Data { get; }
    }

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
