using System;

namespace Message.Contracts
{
    public interface ICapitalizeOrderResponse
    {
        Guid OrderId { get; set; }
        DateTime StartProcessTime { get; set; }
        DateTime EndProcessTime { get; set; }
        string CapitalizeText { get; set; }
    }

    public class CapitalizeOrderResponse : ICapitalizeOrderResponse
    {
        public Guid OrderId { get; set; }
        public DateTime StartProcessTime { get; set; }
        public DateTime EndProcessTime { get; set; }
        public string CapitalizeText { get; set; }
    }
}