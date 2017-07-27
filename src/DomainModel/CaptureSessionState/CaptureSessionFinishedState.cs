namespace DomainModel
{
    internal class CaptureSessionFinishedState : CaptureSessionStateAbstraction
    {
        public CaptureSessionFinishedState(CaptureSession context) : base(context)
        {
        }

        public override CaptureSessionState State => CaptureSessionState.Finished;
    }
}