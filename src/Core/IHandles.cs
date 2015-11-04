using Core.DomainModel;

namespace Core
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}