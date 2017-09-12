using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.WPF.Messages
{
    public class ProcessCommand: IProcessCommand
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
