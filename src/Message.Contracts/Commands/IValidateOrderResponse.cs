using System;
using System.Collections.Generic;
using System.Linq;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IValidateOrderResponse 
    {
        Guid OrderId { get; set; }
        DateTime StartProcessTime { get; set; }
        DateTime EndProcessTime { get; set; }

    }

    public class ValidateOrderResponse : IValidateOrderResponse
    {
        public ValidateOrderResponse(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }

        public DateTime StartProcessTime { get; set; }

        public DateTime EndProcessTime { get; set; }
    }
}