using Core.DomainModel;

namespace Core
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args);
    }



    public interface IHandlesAdded<T> where T : AddedModel<T>
    {
        void Handle(AddedModel<T> args);
    }



    public interface IHandlesUpdatededModel<T> where T : UpdatededModel<T>
    {
        void Handle(UpdatededModel<T> args);
    }
}