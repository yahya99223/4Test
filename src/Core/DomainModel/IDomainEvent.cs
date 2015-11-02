namespace Core.DomainModel
{
    public interface IDomainEvent
    {
    }



    public class AddedModel<T> : IDomainEvent
    {
        public AddedModel(T model)
        {
            Model = model;
        }


        public T Model { get; private set; }
    }



    public class UpdatededModel<T> : IDomainEvent
    {
        public T UpdatedModel { get; private set; }
        public T OldModel { get; private set; }


        public UpdatededModel(T updatedModel, T oldModel)
        {
            UpdatedModel = updatedModel;
            OldModel = oldModel;
        }
    }
}