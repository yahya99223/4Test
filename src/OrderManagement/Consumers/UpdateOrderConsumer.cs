using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Helpers.Core;
using MassTransit;
using Message.Contracts;
using OrderManagement.DbModel;
using OrderManagement.ViewModel;

namespace OrderManagement
{
    public class UpdateOrderConsumer : IConsumer<IOrderValidatedEvent>, IConsumer<IOrderCapitalizedEvent>, IConsumer<IOrderNormalizedEvent>
    {
        private readonly ObservableCollection<OrderViewModel> orders;

        public UpdateOrderConsumer(ObservableCollection<OrderViewModel> orders)
        {
            this.orders = orders;
        }
        public async Task Consume(ConsumeContext<IOrderValidatedEvent> context)
        {
            try
            {
                OrderViewModel orderVM= new OrderViewModel();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    orderVM = orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                    if (orderVM != null)
                    {
                        orderVM.ProcessResults.Add(
                            new ProcessResultViewModel()
                            {
                                IsValid = context.Message.IsValid,
                                ServiceName = Service.Validation.Name,
                                Result = context.Message.IsValid ? "Valid" : $"Invalid: {context.Message.Violations.FriendlyMessage()}"
                            });
                        orderVM.Notifications.Insert(0, context.Message.IsValid ? "Valid" : $"Invalid: {context.Message.Violations.FriendlyMessage()}");
                    }
                });
                using (var dbContext = new OrderManagementDbContext())
                {
                    var order = dbContext.Orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                    if (order != null)
                    {
                        order.ProcessResults.Add(new ProcessResult
                        {
                            Id = Guid.NewGuid(),
                            IsValid = context.Message.IsValid,
                            OrderId = order.Id,
                            Result = context.Message.IsValid ? "Valid" : $"Invalid: {context.Message.Violations.FriendlyMessage()}",
                            ServiceId = Service.Validation.Id,
                        });
                        order.Notifications = string.Join(",", orderVM.Notifications);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Consume(ConsumeContext<IOrderNormalizedEvent> context)
        {
            try
            {
                OrderViewModel orderVM = new OrderViewModel();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    orderVM = orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                    if (orderVM != null)
                    {
                        orderVM.ProcessResults.Add(
                            new ProcessResultViewModel()
                            {
                                IsValid = context.Message.IsValid,
                                Result = context.Message.NormalizedText,
                                ServiceName = Service.Normalize.Name,
                            });
                        orderVM.Notifications.Insert(0, "Order Normalized");
                    }
                });
                using (var dbContext = new OrderManagementDbContext())
                {
                    var order = dbContext.Orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                    if (order != null)
                    {
                        order.ProcessResults.Add(new ProcessResult
                        {
                            Id = Guid.NewGuid(),
                            IsValid = context.Message.IsValid,
                            OrderId = order.Id,
                            Result = context.Message.NormalizedText,
                            ServiceId = Service.Normalize.Id,

                        });
                        order.Notifications = string.Join(",", orderVM.Notifications);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Consume(ConsumeContext<IOrderCapitalizedEvent> context)
        {
            try
            {
                OrderViewModel orderVM = new OrderViewModel();
                Application.Current.Dispatcher.Invoke(() =>
                {
                     orderVM = orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                    if (orderVM != null)
                    {
                        orderVM.ProcessResults.Add(
                            new ProcessResultViewModel()
                            {
                                IsValid = context.Message.IsValid,
                                Result = context.Message.CapitalizedText,
                                ServiceName = Service.Capitalize.Name,
                            });
                        orderVM.Notifications.Insert(0, "Order Capitalized");
                    }
                });
                using (var dbContext = new OrderManagementDbContext())
                {
                    var order = dbContext.Orders.FirstOrDefault(o => o.Id == context.Message.OrderId);
                    if (order != null)
                    {
                        order.ProcessResults.Add(new ProcessResult
                        {
                            Id = Guid.NewGuid(),
                            IsValid = context.Message.IsValid,
                            OrderId = order.Id,
                            Result = context.Message.CapitalizedText,
                            ServiceId = Service.Capitalize.Id,
                        });
                        order.Notifications = string.Join(",", orderVM.Notifications.ToArray());
                        await dbContext.SaveChangesAsync();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}