using System;
using Stateless;

namespace DomainModel
{
    public abstract class CaptureSession
    {
        protected readonly StateMachine<CaptureSessionState, CaptureSessionCommand> machine;
        protected readonly StateMachine<CaptureSessionState, CaptureSessionCommand>.TriggerWithParameters<ProcessRequest> processRequestTrigger;
        private CaptureSessionState state;

        protected CaptureSession(Guid id, CaptureSessionState state)
        {
            Id = id;
            this.state = state;
            machine = new StateMachine<CaptureSessionState, CaptureSessionCommand>(() => State, s => State = s);
            processRequestTrigger = machine.SetTriggerParameters<ProcessRequest>(CaptureSessionCommand.AddProcessRequest);

            machine.Configure(CaptureSessionState.Created)
                .Permit(CaptureSessionCommand.AddProcessRequest, CaptureSessionState.InProgress)
                .Permit(CaptureSessionCommand.Cancel, CaptureSessionState.Cancelled)
                .OnExit(onExit);

            machine.Configure(CaptureSessionState.InProgress)
                .Permit(CaptureSessionCommand.Cancel, CaptureSessionState.Cancelled)
                .OnEntryFrom(processRequestTrigger, onAddProcessRequest)
                .PermitReentryIf(CaptureSessionCommand.AddProcessRequest, canAddRequest)
                .PermitIf(CaptureSessionCommand.Finish, CaptureSessionState.Finished, canFinish)
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
            machine.Fire(CaptureSessionCommand.Cancel);
        }

        protected abstract bool canAddRequest();
        protected abstract bool canCancel();
        protected abstract bool canFinish();
        protected abstract void onAddProcessRequest(ProcessRequest request);

        private void onExit()
        {
            //Console.WriteLine("onExit <-");
        }


        private void unhandledTriggerAction(CaptureSessionState _state, CaptureSessionCommand command)
        {
            throw new Exception($"The CaptureSession is {_state}. It's not allowed to perform {command}");
        }
    }
}