using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IProcessImageCommand
    {
        Guid RequestId { get;  }
        byte[] Data { get; }
    }
}
