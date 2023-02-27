using Core.DomainEvents;
using Core.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Core.EventStore
{
    /// <summary>
    /// EventStore
    /// </summary>
    public class EventStore : IEventStore
    {
        private IMemoryCache MemoryCache { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStore"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        public EventStore(IMemoryCache memoryCache )
        {
            this.MemoryCache = memoryCache;
        }

        /// <summary>
        /// Saves the event.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="domainEvents">The domain events.</param>
        /// <returns></returns>
        public async Task<PersonId> SaveEvent(PersonId personId, IEnumerable<IDomainEvent> domainEvents)
        {
            List<IDomainEvent> events;
            MemoryCache.TryGetValue(personId, out events);

            events = events ?? new List<IDomainEvent>();

            events.AddRange(domainEvents);
            MemoryCache.Set(personId, events, new MemoryCacheEntryOptions().SetSize(1));

            return personId; 
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public async Task<List<IDomainEvent>> GetEvents(PersonId personId)
        {
            List<IDomainEvent> events;
            MemoryCache.TryGetValue(personId, out events);

            return events;
        }
    }
}
