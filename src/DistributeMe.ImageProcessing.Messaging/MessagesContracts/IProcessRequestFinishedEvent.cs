using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessRequestFinishedEvent
    {
        Guid RequestId { get; }
        byte[] Data { get; }
    }
}