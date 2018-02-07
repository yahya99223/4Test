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
        public string Status { get; set; }
        public string Notifications { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<ProcessResult> ProcessResults { get; set; }
    }
}