using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Helpers.Core;
using MassTransit;
using Message.Contracts;
using OrderManagement.DbModel;

namespace OrderManagement.ViewModel
{
    public class CreateOrder : ObservableObject
    {
        private ObservableCollection<ServiceItem> services;
        private ObservableCollection<Order> orders;
        private string textToProcess;

        public CreateOrder()
        {
            ProcessCommand = new AsyncRelayCommand(processCommand);
            orders = new ObservableCollection<Order>();

            using (var dbContext = new OrderManagementDbContext())
            {
                Services = new ObservableCollection<ServiceItem>(dbContext.Services.Select(s => new ServiceItem
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsSelected = true,
                }));

                orders = new ObservableCollection<Order>(dataToRow(dbContext));
            }
        }

        Order toOrder(DbModel.Order order)
        {
            return new Order()
            {
                Notifications = order.Notifications?.Split('&').ToList(),
                Id = order.Id,
                Status = order.Status,
                CreateDate = order.CreateDate,
                FinalResult = order.FinalResult,
                LastUpdateDate = order.LastUpdateDate,
                OriginalText = order.OriginalText,
                ProcessResults = order.ProcessResults,
                Services = order.Services
            };
        }

        DbModel.Order toDB(Order order)
        {
            return new DbModel.Order()
            {
                Notifications = string.Join(",", order.Notifications.ToArray()),
                Id = order.Id,
                Status = order.Status,
                CreateDate = order.CreateDate,
                FinalResult = order.FinalResult,
                LastUpdateDate = order.LastUpdateDate,
                OriginalText = order.OriginalText,
                ProcessResults = order.ProcessResults,
                Services = order.Services
            };
        }
        public List<Order> dataToRow(OrderManagementDbContext context)
        {
            List<Order> orders = new List<Order>();
            foreach (var order in context.Orders)
            {
                orders.Add(toOrder(order));
            }
            return orders;
        }
        public IEnumerable<ServiceItem> Services
        {
            get { return services; }
            set
            {
                services = new ObservableCollection<ServiceItem>(value);
                RaisePropertyChanged("Services");
            }
        }

        public ICommand ProcessCommand { get; }

        public string TextToProcess
        {
            get { return textToProcess; }
            set
            {
                textToProcess = value;
                RaisePropertyChanged("TextToProcess");
            }
        }

        public ObservableCollection<Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                RaisePropertyChanged("Orders");
            }
        }

        private async Task processCommand(object arg)
        {
            var servicesIds = Services.Where(s => s.IsSelected).Select(s => s.Id).ToHashSet();
            using (var dbContext = new OrderManagementDbContext())
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Services = dbContext.Services.Where(s => servicesIds.Contains(s.Id)).ToList(),
                    CreateDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow,
                    OriginalText = TextToProcess,
                    Status = "Created",
                };
                dbContext.Orders.Add(toDB(order));
                TextToProcess = null;
                await dbContext.SaveChangesAsync();
                Orders.Add(new Order()
                {
                    Id = order.Id,
                });

                await MainWindow.BusControl.Publish<IOrderCreatedEvent>(new OrderCreated
                {
                    OrderId = order.Id,
                    CreateDate = order.CreateDate,
                    OriginalText = order.OriginalText,
                    Services = order.Services.Select(s => s.Name).ToList()
                });
            }
        }
    }
    public class Order
    {
        public Order()
        {
            Services = new HashSet<Service>();
            ProcessResults = new HashSet<ProcessResult>();
            Notifications = new List<string>();
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string OriginalText { get; set; }
        public string FinalResult { get; set; }
        public string Status { get; set; }
        public ICollection<string> Notifications { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<ProcessResult> ProcessResults { get; set; }
    }
}