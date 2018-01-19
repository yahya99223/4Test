namespace Helpers.Core
{
    public abstract class Entity<TId>
    {
        protected Entity()
        {

        }

        protected Entity(TId id)
        {
            if (Equals(id, default(TId)))
                throw new InternalApplicationException<Entity<TId>>(x => x.Id, ViolationType.Required);

            Id = id;
        }


        public virtual TId Id { get; protected set; }
    }
}