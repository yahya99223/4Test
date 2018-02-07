using System;
using System.Collections.ObjectModel;
using OrderManagement.DbModel;

namespace OrderManagement.ViewModel
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderServices = new ObservableCollection<Service>();
            ProcessResults = new ObservableCollection<ProcessResultViewModel>();
            Notifications = new ObservableCollection<string>();
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string OriginalText { get; set; }
        public string Status { get; set; }
        public ObservableCollection<string> Notifications { get; set; }
        public ObservableCollection<Service> OrderServices { get; set; }
        public ObservableCollection<ProcessResultViewModel> ProcessResults { get; set; }
    }
}