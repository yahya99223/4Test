using System;

namespace DomainModel
{
    public abstract class CaptureSession
    {
        protected CaptureSessionStateAbstraction machine;

        protected CaptureSession(Guid id, CaptureSessionState state)
        {
            Id = id;
            machine = CaptureSessionStateAbstraction.GetState(state, this);
        }

        public Guid Id { get; }

        public CaptureSessionState State => machine.State;

        public abstract ProcessRequest[] ProcessRequests { get; }
        public abstract ProcessResult[] ProcessResults { get; }
        public abstract CaptureSessionResult Result { get; }
        public abstract void AddProcessRequest(string requestData);

        public virtual void Cancel()
        {
            machine.Cancel();
        }

        protected internal abstract bool canAddRequest();
        protected internal abstract bool canCancel();
        protected internal abstract bool canFinish();


        internal void setState(CaptureSessionStateAbstraction newState)
        {
            machine = newState;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Capture Session state is {newState.State}");
            Console.WriteLine();
        }
    }
}