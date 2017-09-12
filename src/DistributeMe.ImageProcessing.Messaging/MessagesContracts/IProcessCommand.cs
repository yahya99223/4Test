using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessCommand
    {
        Guid RequestId { get;  }
        byte[] Data { get; }
    }
}