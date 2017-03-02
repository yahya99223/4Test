namespace Shared.Messaging
{
    public static class MessagingConstants
    {
        public const string MqUri = "rabbitmq://localhost/DistributeMe/";
        public const string UserName = "wahid";
        public const string Password = "poi098";


        public const string MessageManagingQueue = "MessageManaging.App1";
        public const string MessageProcessQueue = "MessageProcess.App2";

    }
}