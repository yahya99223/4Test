using System;
using System.Linq;
using System.Threading.Tasks;
using Helpers.Core;
using MassTransit;
using Message.Contracts;
using OrderManagement.DbModel;

namespace OrderManagement
{
    public class UpdateOrderConsumer : IConsumer<IOrderValidatedEvent>, IConsumer<IOrderCapitalizedEvent>, IConsumer<IOrderNormalizedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderValidatedEvent> context)
        {
            try
            {
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
                            ServiceId = Service.Capitalize.Id,
                        });
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