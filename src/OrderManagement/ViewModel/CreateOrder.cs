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
        private ObservableCollection<ProcessRequest> processRequests;
        private string textToProcess;

        public CreateOrder()
        {
            ProcessCommand = new AsyncRelayCommand(processCommand);
            ProcessRequests = new ObservableCollection<ProcessRequest>();

            Task.Run(() =>
            {
                using (var dbContext = new OrderManagementDbContext())
                {
                    Services = new ObservableCollection<ServiceItem>(dbContext.Services.Select(s => new ServiceItem
                    {
                        Id = s.Id,
                        Name = s.Name,
                        IsSelected = true,
                    }));

                    ProcessRequests = new ObservableCollection<ProcessRequest>(dbContext.Orders
                        .Where(o => o.Status != "Finished")
                        .ToList()
                        .Select(x => new ProcessRequest
                        {
                            RequestId = x.Id,
                            //NotificationString = x.Notifications,
                        }));

                    /*foreach (var processRequest in dbContext.Orders
                        .Where(o => o.Status != "Finished")
                        .ToList()
                        .Select(x => new ProcessRequest
                        {
                            RequestId = x.Id,
                            //NotificationString = x.Notifications,
                        }))
                    {
                        ProcessRequests.Add(processRequest);
                    }*/
                }
            });
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

        public ObservableCollection<ProcessRequest> ProcessRequests
        {
            get { return processRequests; }
            set
            {
                processRequests = value;
                RaisePropertyChanged("ProcessRequests");
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
                dbContext.Orders.Add(order);
                TextToProcess = null;
                await dbContext.SaveChangesAsync();
                ProcessRequests.Add(new ProcessRequest
                {
                    RequestId = order.Id,
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
}