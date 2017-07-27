namespace DomainModel
{
    internal class CaptureSessionCancelledState : CaptureSessionStateAbstraction
    {
        public CaptureSessionCancelledState(CaptureSession context) : base(context)
        {
        }

        public override CaptureSessionState State => CaptureSessionState.Cancelled;
    }
}