using Core.DomainEvents;
using Core.ValueObjects;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Core.EventStore;

namespace Core.Person
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class BaseEntity<TKey>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


        public TKey Id { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{TKey}"/> class.
        /// </summary>
        public BaseEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{TKey}"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public BaseEntity(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyOnEntity(@event);
            }
        }

        /// <summary>
        /// Adds the domain event.
        /// </summary>
        /// <param name="event">The event.</param>
        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        /// <summary>
        /// Removes the domain event.
        /// </summary>
        /// <param name="event">The event.</param>
        protected void RemoveDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Remove(@event);
        }

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="event">The event.</param>
        protected void AddEvent(IDomainEvent @event)
        {
            ApplyOnEntity(@event);
            AddDomainEvent(@event);
        }

        /// <summary>
        /// Applies the on entity.
        /// </summary>
        /// <param name="event">The event.</param>
        private void ApplyOnEntity(IDomainEvent @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
        }
    }
}
