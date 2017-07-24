using System;
using Stateless;

namespace DomainModel
{
    public abstract class CaptureSession
    {
        protected readonly StateMachine<CaptureSessionState, DocumentCaptureSessionCommand> machine;
        protected readonly StateMachine<CaptureSessionState, DocumentCaptureSessionCommand>.TriggerWithParameters<ProcessRequest> processRequestTrigger;
        private CaptureSessionState state;

        protected CaptureSession(Guid id, CaptureSessionState state)
        {
            Id = id;
            this.state = state;
            machine = new StateMachine<CaptureSessionState, DocumentCaptureSessionCommand>(() => State, s => State = s);
            processRequestTrigger = machine.SetTriggerParameters<ProcessRequest>(DocumentCaptureSessionCommand.AddProcessRequest);

            machine.Configure(CaptureSessionState.Created)
                .Permit(DocumentCaptureSessionCommand.AddProcessRequest, CaptureSessionState.InProgress)
                .Permit(DocumentCaptureSessionCommand.Cancel, CaptureSessionState.Cancelled)
                .OnExit(onExit);

            machine.Configure(CaptureSessionState.InProgress)
                .Permit(DocumentCaptureSessionCommand.Cancel, CaptureSessionState.Cancelled)
                .OnEntryFrom(processRequestTrigger, onAddProcessRequest)
                .PermitReentryIf(DocumentCaptureSessionCommand.AddProcessRequest, canAddRequest)
                .PermitIf(DocumentCaptureSessionCommand.Finish, CaptureSessionState.Finished, canFinish)
                .OnExit(onExit);

            machine.OnUnhandledTrigger(unhandledTriggerAction);
        }

        public Guid Id { get; }

        public CaptureSessionState State
        {
            get => state;
            protected set
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Capture Session state is {value}");
                Console.WriteLine();
                state = value;
            }
        }

        public abstract ProcessRequest[] ProcessRequests { get; }
        public abstract ProcessResult[] ProcessResults { get; }
        public abstract CaptureSessionResult Result { get; }
        public abstract void AddProcessRequest(string requestData);

        public virtual void Cancel()
        {
            machine.Fire(DocumentCaptureSessionCommand.Cancel);
        }

        protected abstract bool canAddRequest();
        protected abstract bool canCancel();
        protected abstract bool canFinish();
        protected abstract void onAddProcessRequest(ProcessRequest request);

        private void onExit()
        {
            //Console.WriteLine("onExit <-");
        }


        private void unhandledTriggerAction(CaptureSessionState _state, DocumentCaptureSessionCommand command)
        {
            throw new Exception($"The CaptureSession is {_state}. It's not allowed to perform {command}");
        }
    }
}