using System;

namespace Message.Contracts
{
    public interface ICapitalizeOrderCommand
    {
        Guid OrderId { get; set; }
        string OriginalText { get; set; }
    }

    public class CapitalizeOrderCommand : ICapitalizeOrderCommand
    {
        public Guid OrderId { get; set; }
        public string OriginalText { get; set; }
    }
}