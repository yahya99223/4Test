using System;
using System.Collections.Generic;

namespace OrderManagement.DbModel
{
    public class Order
    {
        public Order()
        {
            Services = new HashSet<Service>();
            ProcessResults = new HashSet<ProcessResult>();
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }        
        public string OriginalText { get; set; }
        public string FinalResult { get; set; }
        public string Status { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<ProcessResult> ProcessResults { get; set; }
    }

    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

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