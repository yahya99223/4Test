namespace ConsoleApp.Model
{
    internal enum CaptureSessionState
    {
        Created,
        InProgress,
        Finished
    }

    internal enum DocumentCaptureSessionCommand
    {
        AddProcessRequest,
        Finish
    }

    internal enum CaptureSessionResult
    {
        Undetermined,
        Passed,
        Failed
    }

    internal enum PageResult
    {
        Undetermined,
        Passed,
        Failed
    }

    internal enum PageState
    {
        Created,
        InProgress,
        Finished
    }

    internal enum PageCommand
    {
        TryProcess,
        Finish
    }
}