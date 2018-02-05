using System;

namespace OrderManagement.ViewModel
{
    public class ServiceItem 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}