using System;
using Automatonymous;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.Saga
{
    public class AddingProcessRequestStateMachine : MassTransitStateMachine<AddingProcessRequestSagaState>
    {
        private Event processFinished;

        public AddingProcessRequestStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => ProcessCommandReceived,
                x => x.CorrelateBy(business => business.RequestId.ToString(), context => context.Message.RequestId.ToString())
                    .SelectId(context => context.Message.RequestId));

            Event(() => OcrProcessed, x => x.CorrelateById(context => context.Message.RequestId));
            Event(() => FaceRecognitionProcessed, x => x.CorrelateById(context => context.Message.RequestId));

            Initially(
                When(ProcessCommandReceived)
                    .Then(context =>
                    {
                        context.Instance.StartProcessDate = DateTime.Now;
                        context.Instance.RequestId = context.Data.RequestId;
                    })
                    .Then(context => Console.Out.WriteLine($"ProcessCommandReceived - RequestId: {context.Data.RequestId} With CorrelationId: {context.Instance.CorrelationId}"))
                    .TransitionTo(Received)
                    .Publish(context => new ProcessRequestAddedEvent(context.Instance.RequestId, null))
            );


            During(Received,
                When(OcrProcessed)
                    .Then(context =>
                    {
                        Console.WriteLine($"OcrProcessed - RequestId: {context.Data.RequestId} With CorrelationId: {context.Instance.CorrelationId}");
                    })
                //.If(x => x.Instance.FaceFinished, x => x.TransitionTo(Finished))
                ,
                When(FaceRecognitionProcessed)
                    .Then(context =>
                    {
                        Console.WriteLine($"FaceProcessed - RequestId: {context.Data.RequestId} With CorrelationId: {context.Instance.CorrelationId}");
                    })
            );
            
            CompositeEvent(() => ProcessFinished, x => x.RequestFinishedStatus, FaceRecognitionProcessed, OcrProcessed);

            During(Received,
                When(ProcessFinished)
                    .Then(context =>
                    {
                        Console.WriteLine($"FinishProcessing - CorrelationId: {context.Instance.CorrelationId}");
                        context.Instance.EndProcessDate = DateTime.Now;
                    })
                    .Publish(context => new ProcessRequestFinishedEvent(context.Instance.RequestId, null))
                    .Finalize()
            );
            //SetCompletedWhenFinalized();
        }


        public State Received { get; private set; }

        public Event<IProcessCommand> ProcessCommandReceived { get; private set; }
        public Event<IFaceRecognitionImageProcessedEvent> FaceRecognitionProcessed { get; private set; }
        public Event<IOcrImageProcessedEvent> OcrProcessed { get; private set; }

        public Event ProcessFinished
        {
            get { return processFinished; }
            set
            {
                processFinished = value;
            }
        }
    }
}