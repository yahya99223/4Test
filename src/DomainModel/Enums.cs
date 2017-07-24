namespace DomainModel
{
    public enum CaptureSessionState
    {
        Created,
        InProgress,
        Finished,
        Cancelled
    }

    public enum DocumentCaptureSessionCommand
    {
        AddProcessRequest,
        Finish,
        Cancel
    }

    public enum CaptureSessionResult
    {
        Undetermined,
        Passed,
        Failed
    }

    public enum PageResult
    {
        Undetermined,
        Passed,
        Failed
    }

    public enum PageState
    {
        Created,
        InProgress,
        Finished
    }

    public enum PageCommand
    {
        TryProcess,
        Finish
    }
}