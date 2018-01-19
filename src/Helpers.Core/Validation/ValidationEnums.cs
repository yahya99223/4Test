namespace Helpers.Core
{
    public enum ViolationLevel
    {
        Info = 0,
        Warning = 1,
        Error = 2,
    }



    public enum ViolationType
    {
        General,
        DatabaseException,
        Required,
        Invalid,
        Duplicated,
        NotFound,
        MaxLength,
        MinLength,
        NotAllowed,
        NotInitialized,
        Null,
        NotMatched,
        InActive,
        AlreadyInProgress,
        AccessError,
        ShouldBeBigger,
        StorageModule,
        NotSupported
    }
}