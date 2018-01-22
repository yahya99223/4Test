using System;

namespace Message.Contracts
{
    public interface IValidateOrderResponse
    {
        Guid OrderId { get; set; }
        DateTime StartProcessTime { get; set; }
        DateTime EndProcessTime { get; set; }
        bool IsValid { get; set; }
        string Errors { get; set; }
    }
}