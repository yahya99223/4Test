using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessRequestAddedEvent
    {
        Guid RequestId { get; }
        byte[] Data { get; }
    }
}
