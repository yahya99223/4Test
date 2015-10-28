namespace DomainModel
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args); 
    }
}