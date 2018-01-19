namespace Helpers.Core
{
    public abstract class ValidatableEntity<TId, TModel> : Validatable<TModel> where TModel : class
    {
        protected ValidatableEntity()
        {
            Id = default(TId);
        }

        protected ValidatableEntity(TId id)
        {
            if (Equals(id, default(TId)))
                throw new InternalApplicationException<ValidatableEntity<TId, TModel>>(x => x.Id, ViolationType.Required);

            Id = id;
        }

        public TId Id { get; protected set; }
    }
}