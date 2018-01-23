using System;
using System.Collections.Generic;

namespace Message.Contracts
{
    public interface IValidateOrderResponse
    {
        Guid OrderId { get; set; }
        DateTime StartProcessTime { get; set; }
        DateTime EndProcessTime { get; set; }
        bool IsValid { get; set; }
        IList<string> Errors { get; set; }
    }

    public class ValidateOrderResponse : IValidateOrderResponse
    {
        public ValidateOrderResponse()
        {
            Errors = new List<string>();
        }

        public Guid OrderId { get; set; }
        public DateTime StartProcessTime { get; set; }
        public DateTime EndProcessTime { get; set; }
        public bool IsValid { get; set; }
        public IList<string> Errors { get; set; }
    }
}