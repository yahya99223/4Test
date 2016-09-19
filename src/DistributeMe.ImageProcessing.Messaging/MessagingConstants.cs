using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Messaging
{
    public static class MessagingConstants
    {
        public const string MqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        

        public const string ProcessFaceQueue = "imageprocessing.processface.queue";
        public const string ProcessOcrQueue = "imageprocessing.processocr.queue";

        public const string ProcessedFaceNotificationQueue = "imageprocessing.notification.processedface.queue";
        public const string ProcessedOcrNotificationQueue = "imageprocessing.notification.processedocr.queue";
    }
}
