using System;

namespace Message.Contracts
{
    public interface IValidateOrderCommand
    {
        Guid OrderId { get; set; }
        string OriginalText { get; set; }
    }

    public class ValidateOrderCommand : IValidateOrderCommand
    {
        public Guid OrderId { get; set; }
        public string OriginalText { get; set; }
    }
}
