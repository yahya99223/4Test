using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Helpers.Core;
using MassTransit;
using Message.Contracts;
using OrderManagement.ViewModel;

namespace OrderManagement
{
    public class NotificationConsumer : IConsumer<IOrderValidatedEvent>, IConsumer<IOrderCapitalizedEvent>, IConsumer<IOrderNormalizedEvent>
    {
        private readonly ObservableCollection<OrderViewModel> orders;

        public NotificationConsumer(ObservableCollection<OrderViewModel> orders)
        {
            this.orders = orders;
        }

        public Task Consume(ConsumeContext<IOrderValidatedEvent> context)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var order = orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                if (order != null)
                {
                    order.Notifications.Insert(0, context.Message.IsValid ? "Valid" : $"Invalid: {context.Message.Violations.FriendlyMessage()}");
                }
            });

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<IOrderNormalizedEvent> context)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var order = orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                if (order != null)
                {
                    order.Notifications.Insert(0, "Order Normalized");
                }
            });
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<IOrderCapitalizedEvent> context)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var order = orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                if (order != null)
                {
                    order.Notifications.Insert(0, "Order Capitalized");
                }
            });

            return Task.CompletedTask;
        }
    }
}