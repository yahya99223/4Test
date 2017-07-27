using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public abstract class CaptureSessionStateAbstraction
    {
        protected readonly CaptureSession context;

        public abstract CaptureSessionState State { get; }

        protected CaptureSessionStateAbstraction(CaptureSession context)
        {
            this.context = context;
        }

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
    }
}
