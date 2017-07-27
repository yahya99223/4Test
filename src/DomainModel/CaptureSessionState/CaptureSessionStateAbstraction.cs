using System;

namespace DomainModel
{
    public abstract class CaptureSessionStateAbstraction
    {
        public static CaptureSessionStateAbstraction GetState(CaptureSessionState state, CaptureSession context)
        {
            switch (state)
            {
                case CaptureSessionState.Created:
                    return new CaptureSessionCreatedState(context);

                case CaptureSessionState.InProgress:
                    return new CaptureSessionInProgressState(context);

                case CaptureSessionState.Finished:
                    return new CaptureSessionFinishedState(context);

                case CaptureSessionState.Cancelled:
                    return new CaptureSessionCancelledState(context);

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        protected readonly CaptureSession context;

        protected CaptureSessionStateAbstraction(CaptureSession context)
        {
            this.context = context;
        }

        public abstract CaptureSessionState State { get; }

        public virtual void Proceed()
        {
            throw new Exception($"It's not allowed to change to {CaptureSessionState.InProgress} from {context.State}");
        }

        public virtual void Finish()
        {
            throw new Exception($"It's not allowed to change to {CaptureSessionState.Finished} from {context.State}");
        }

        public virtual void Cancel()
        {
            throw new Exception($"It's not allowed to change to {CaptureSessionState.Cancelled} from {context.State}");
        }
    }
}