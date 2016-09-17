using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.WPF.Messages
{
    public class ProcessImageCommand: IProcessImageCommand
    {
        public Guid RequestId { get; set; }
        public byte[] Data { get; set; }
    }
}
