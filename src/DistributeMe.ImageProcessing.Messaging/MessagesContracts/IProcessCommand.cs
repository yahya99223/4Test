using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessCommand
    {
        Guid RequestId { get; }
        byte[] Data { get; }
    }

    public class ProcessCommand : IProcessCommand
    {
        public ProcessCommand(Guid requestId, byte[] data)
        {
            RequestId = requestId;
            Data = data;
        }

        public Guid RequestId { get; }
        public byte[] Data { get; }
    }
}