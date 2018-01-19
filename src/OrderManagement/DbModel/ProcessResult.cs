using System;

namespace OrderManagement.DbModel
{
    public class ProcessResult
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ServiceId { get; set; }
        public string Result { get; set; }
        public bool IsValid { get; set; }
        public Service Service { get; set; }
        public Order Order { get; set; }
    }
}