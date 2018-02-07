using System;
using System.Collections.Generic;

namespace Saga.Service
{
    public interface IOrderReadyToProcessEvent
    {
        Guid OrderId { get; set; }
        string OriginalText { get; set; }
        ICollection<string> Services { get; set; }
    }

    public class OrderReadyToProcessEvent : IOrderReadyToProcessEvent
    {
        public Guid OrderId { get; set; }
        public string OriginalText { get; set; }
        public ICollection<string> Services { get; set; }
    }
}