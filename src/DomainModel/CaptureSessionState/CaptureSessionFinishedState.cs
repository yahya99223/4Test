namespace DomainModel
{
    class CaptureSessionFinishedState : CaptureSessionStateAbstraction
    {
        public CaptureSessionFinishedState(CaptureSession context) : base(context)
        {
        }

        public override CaptureSessionState State
        {
            get { return CaptureSessionState.Finished; }
        }
    }
}