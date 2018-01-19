namespace Helpers.Core
{
    public interface IServiceEventHandler<T> where T : IServiceEvent
    {
        void Handle(T args);
    }
}