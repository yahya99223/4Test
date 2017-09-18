using System;
using Automatonymous;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;
using MassTransit.EntityFrameworkIntegration;
using MassTransit.EntityFrameworkIntegration.Saga;
using MassTransit.Saga;
using Topshelf;
using Topshelf.Logging;

namespace DistributeMe.Saga
{
    internal class SagaService: ServiceControl
    {
        private readonly LogWriter log = HostLogger.Get<SagaService>();
        
        private BusHandle busHandle;
        private AddingProcessRequestStateMachine machine;
        private Lazy<ISagaRepository<AddingProcessRequestSagaState>> repository;


        public bool Start(HostControl hostControl)
        {
            log.Info("Creating bus...");
            machine = new AddingProcessRequestStateMachine();
            SagaDbContextFactory sagaDbContextFactory = () => new SagaDbContext<AddingProcessRequestSagaState, AddingProcessRequestSagaMap>(SagaDbContextFactoryProvider.ConnectionString);
            repository = new Lazy<ISagaRepository<AddingProcessRequestSagaState>>(() => new EntityFrameworkSagaRepository<AddingProcessRequestSagaState>(sagaDbContextFactory));
            //repository = new InMemorySagaRepository<AddingProcessRequestSagaState>();

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, MessagingConstants.SagaQueue, e =>
                {
                    e.Consumer<ProcessFaultConsumer>();
                    e.StateMachineSaga(machine, repository.Value); 
                });
            });
            log.Info("Starting bus...");
            busHandle = bus.StartAsync().Result;
            return true;

        }

        public bool Stop(HostControl hostControl)
        {
            log.Info("Stopping bus...");            
            if (busHandle != null)
                busHandle.Stop();            

            return true;
        }
    }
}