using Core.EventStore;
using Core.Person;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// PersonRepository
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly IEventStore _eventStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        public PersonRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public async Task<Person> GetPerson(Guid personId)
        {
            var personEvents = await _eventStore.GetEvents((PersonId)personId);

            if (personEvents != null)
                return new Person(personEvents);

            return null;
        }

        /// <summary>
        /// Saves the person asynchronous.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public async Task<PersonId> SavePersonAsync(Person person)
        {
            await _eventStore.SaveEvent(person.Id, person.DomainEvents);
            return person.Id;
        }
    }
}
