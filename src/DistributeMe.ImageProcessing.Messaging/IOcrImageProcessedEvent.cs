using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IOcrImageProcessedEvent
    {
        Guid RequestId { get; }
        string ExtractedText { get; }
        DateTime ProcessStartTime { get; }
        DateTime ProcessFinishTime { get; }
    }
}
