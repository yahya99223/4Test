
namespace Message.Contracts
{
    public static class MessagingConstants
    {
        public const string MqUri = "rabbitmq://onboarding-db/IDScanSaaSYahya/";
        public const string UserName = "yahya";
        public const string Password = "123123";


        public const string OrderManagementQueue = "OrderManagement";
        public const string CapitalizeServiceQueue = "Capitalize.Service";
        public const string NormalizeServiceQueue = "Normalize.Service";
        public const string ValidateServiceQueue = "Validate.Service";
        public const string SagaQueue = "Saga.Service";
    }
}
