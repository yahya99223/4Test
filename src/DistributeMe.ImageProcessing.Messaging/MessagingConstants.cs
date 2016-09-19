using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Messaging
{
    public static class MessagingConstants
    {
        public const string MqUri = "rabbitmq://localhost/distributeme/";
        public const string UserName = "distributeme";
        public const string Password = "poi098";
        

        public const string ProcessFaceQueue = "distributeme.imageprocessing.processface.queue";
        public const string ProcessOcrQueue = "distributeme.imageprocessing.processocr.queue";

        public const string ProcessedFaceNotificationQueue = "distributeme.imageprocessing.processedfacenotification.queue";
        public const string ProcessedOcrNotificationQueue = "distributeme.imageprocessing.processedocrnotification.queue";
    }
}
