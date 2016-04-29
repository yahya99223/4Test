namespace Core
{
    public interface IHandles<T> where T : IDomainEvent
    {
        bool IsAsync { get; }
        void Handle(T args); 
    }
}