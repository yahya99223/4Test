using System;

namespace OrderManagement.ViewModel
{
    public class ServiceItem : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}