
namespace DistributeMe.ImageProcessing.Messaging
{
    public static class MessagingConstants
    {
        public const string MqUri = "rabbitmq://localhost/DistributeMe/";
        public const string UserName = "wahid";
        public const string Password = "poi098";


        public const string ProcessFaceQueue = "FaceProcessing.Service";
        public const string ProcessOcrQueue = "Ocr.Service";
        public const string NotificationQueue = "Notification.Service";
        public const string SagaQueue = "Saga.Service";
    }
}
