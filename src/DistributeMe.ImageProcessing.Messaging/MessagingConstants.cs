
namespace DistributeMe.ImageProcessing.Messaging
{
    public static class MessagingConstants
    {
        public const string MqUri = "rabbitmq://http://onboarding-db/IDScanSaaSWahid/";
        public const string UserName = "wahid";
        public const string Password = "W@123123";


        public const string ProcessFaceQueue = "FaceProcessing.Service";
        public const string ProcessOcrQueue = "Ocr.Service";
        public const string NotificationQueue = "Notification.Service";
        public const string SagaQueue = "Saga.Service";
    }
}
