namespace DomainModel
{
    class CaptureSessionCancelledState : CaptureSessionStateAbstraction
    {
        public CaptureSessionCancelledState(CaptureSession context) : base(context)
        {
        }

        public override CaptureSessionState State
        {
            get { return CaptureSessionState.Cancelled; }
        }
    }
}