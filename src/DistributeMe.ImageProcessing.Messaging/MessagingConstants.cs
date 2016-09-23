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
        public const string UserName = "wahid";
        public const string Password = "poi098";
        

        public const string ProcessFaceQueue = "imageprocessing.face.service";
        public const string ProcessOcrQueue = "imageprocessing.ocr.service";
    }
}
