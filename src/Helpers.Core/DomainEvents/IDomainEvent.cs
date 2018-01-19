using System;

namespace Helpers.Core
{
    public interface IDomainEvent
    {
        Guid EventKey { get; set; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent() : this(null)
        {
        }

        protected DomainEvent(Guid? eventKey)
        {
            EventKey = eventKey ?? Guid.NewGuid();
        }

        public Guid EventKey { get; set; }
    }
}