using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Messaging
{
    public static class MessagingConstants
    {
        public const string MqUri = "amqp://destributeme:poi098@localhost:5672/";
        public const string ContentType = "application/json";

        public const string ProcessImageExchange = "distributeme.imageprocessing.processimage.exchange";
        //public const string ProcessedFaceExchange = "distributeme.imageprocessing.processedface.exchange";
        //public const string ProcessedOcrExchange = "distributeme.imageprocessing.processedocr.exchange";

        public const string ProcessFaceQueue = "distributeme.imageprocessing.processface.queue";
        public const string ProcessOcrQueue = "distributeme.imageprocessing.processocr.queue";

        public const string ProcessedFaceNotificationQueue = "distributeme.imageprocessing.processedfacenotification.queue";
        public const string ProcessedOcrNotificationQueue = "distributeme.imageprocessing.processedocrnotification.queue";
    }
}
