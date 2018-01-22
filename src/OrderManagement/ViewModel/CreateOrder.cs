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
    public class CreateOrder : ObservableObject, IDisposable
    {
        private readonly ObservableCollection<ServiceItem> services;
        private readonly OrderManagementDbContext dbContext;
        private string textToProcess;
        private IBusControl bus;

        public CreateOrder()
        {
            bus = BusHelper.GetBusControl();
            ProcessCommand = new AsyncRelayCommand(processCommand);

            dbContext = new OrderManagementDbContext();
            services = new ObservableCollection<ServiceItem>(dbContext.Services.Select(s => new ServiceItem
            {
                Id = s.Id,
                Name = s.Name,
                IsSelected = true,
            }));
        }

        public IEnumerable<ServiceItem> Services
        {
            get { return services; }
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

        private async Task processCommand(object arg)
        {
            var servicesIds = Services.Where(s => s.IsSelected).Select(s => s.Id).ToHashSet();
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
            await bus.Publish<IOrderCreatedEvent>(new OrderCreated
            {
                OrderId = order.Id,
                CreateDate = order.CreateDate,
                OriginalText = order.OriginalText,
                Services = order.Services.Select(s => s.Name).ToList()
            });
        }

        public void Dispose()
        {
            dbContext?.Dispose();
            bus?.Stop();
        }
    }
}