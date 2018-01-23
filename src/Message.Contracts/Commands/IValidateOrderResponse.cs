using System;
using System.Collections.Generic;
using System.Linq;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IValidateOrderResponse : IValidatedMessage
    {
        Guid OrderId { get; set; }
        DateTime StartProcessTime { get; set; }
        DateTime EndProcessTime { get; set; }

    }

    public class ValidateOrderResponse : ValidatedMessage, IValidateOrderResponse
    {
        public ValidateOrderResponse(IList<IViolation> violations) : base(violations)
        {
        }

        public Guid OrderId { get; set; }

        public DateTime StartProcessTime { get; set; }

        public DateTime EndProcessTime { get; set; }
    }
}