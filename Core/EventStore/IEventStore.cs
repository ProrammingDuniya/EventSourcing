using Core.DomainEvents;
using Core.ValueObjects;

namespace Core.EventStore
{
    /// <summary>
    /// IEventStore
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Saves the event.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        public Task<PersonId> SaveEvent(PersonId personId, IEnumerable<IDomainEvent> @event);

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public Task<List<IDomainEvent>> GetEvents(PersonId personId);
    }
}
