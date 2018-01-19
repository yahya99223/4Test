using System;

namespace Message.Contracts.Events
{
    public interface IOrderValidated
    {
        Guid OrderId { get; set; }
        bool IsValid { get; set; }
    }

    public class OrderValidated : IOrderValidated
    {
        public Guid OrderId { get; set; }
        public bool IsValid { get; set; }
    }
}