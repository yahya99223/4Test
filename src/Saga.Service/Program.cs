using System;
using Automatonymous;
using Helpers.Core;
using MassTransit;
using MassTransit.EntityFrameworkIntegration;
using MassTransit.EntityFrameworkIntegration.Saga;
using MassTransit.Saga;
using Message.Contracts;

namespace Saga.Service
{
    class Program
    {
        private static BusHandle busHandle;

        private static OrderCreatedStateMachine machine;

        //private static ISagaRepository<OrderCreatedSagaState> repository;
        private static Lazy<ISagaRepository<OrderCreatedSagaState>> lazyRepository;

        static void Main(string[] args)
        {
            machine = new OrderCreatedStateMachine();
            //repository = new InMemorySagaRepository<OrderCreatedSagaState>();
            SagaDbContextFactory sagaDbContextFactory = () => new SagaDbContext<OrderCreatedSagaState, OrderCreatedSagaSagaMap>(SagaDbContextFactoryProvider.ConnectionString);
            lazyRepository = new Lazy<ISagaRepository<OrderCreatedSagaState>>(() => new EntityFrameworkSagaRepository<OrderCreatedSagaState>(sagaDbContextFactory));

            var bus = BusConfigurator.ConfigureBus(MessagingConstants.MqUri, MessagingConstants.UserName, MessagingConstants.Password, (cfg, host) =>
            {
                //cfg.UseRetry(retryConfig => retryConfig.Interval(7, TimeSpan.FromSeconds(5)));

                cfg.ReceiveEndpoint(host, MessagingConstants.SagaQueue, e =>
                {
                    //e.StateMachineSaga(machine, repository);
                    e.StateMachineSaga(machine, lazyRepository.Value);
                });
            });
            busHandle = bus.StartAsync().Result;
        }
    }

    public class OrderCreatedSagaSagaMap : SagaClassMapping<OrderCreatedSagaState>
    {
        public OrderCreatedSagaSagaMap()
        {
            Property(x => x.CurrentState).HasMaxLength(64);
            Property(x => x.OrderId);
            Property(x => x.CreateDate);
            Property(x => x.OriginalText);
            Property(x => x.RemainingServices);
            Property(x => x.RequestFinishedStatusBits);
        }
    }
}






