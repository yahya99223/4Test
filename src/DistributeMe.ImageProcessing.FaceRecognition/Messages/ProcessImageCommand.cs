using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.FaceRecognition.Messages
{
    public class ProcessImageCommand : IProcessImageCommand
    {
        public Guid RequestId { get; set; }
        public byte[] Data { get; set; }
    }
}
