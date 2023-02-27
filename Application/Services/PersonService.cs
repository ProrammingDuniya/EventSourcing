using Application.Model;
using Core.EventStore;
using Core.Person;
using Core.ValueObjects;
using Infrastructure.Repositories;

namespace Application.Services
{
    /// <summary>
    /// PersonService
    /// </summary>
    public class PersonService : IPersonService
    {
        private IEventStore _eventStore { get; }
        private IPersonRepository _personRepository { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="personRepository">The person repository.</param>
        public PersonService(IEventStore eventStore, IPersonRepository personRepository)
        {
            _eventStore = eventStore;
            _personRepository= personRepository; 
        }

        /// <summary>
        /// Creates the person.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public async Task<PersonId> CreatePerson(string firstName, string lastName)
        {
            var person = Person.CreateNewPerson(firstName, lastName);

            var personalId = await _eventStore.SaveEvent(person.Id, person.DomainEvents);

            return personalId;
        }

        /// <summary>
        /// Gets the person data.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public async Task<PersonReadModel> GetPersonData(Guid personId)
        {
            var person = await _personRepository.GetPerson(personId);

            if (person == null) 
                return new PersonReadModel(); // throw not found exception

            return new PersonReadModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonId = person.Id.ToString(),
                Address = person.PersonAddress != null ? new AddressModel()
                {
                    City = person.PersonAddress?.City,
                    Country = person.PersonAddress?.Country,
                    Street = person.PersonAddress?.Street,
                    ZipCode = person.PersonAddress?.ZipCode,
                    State = person.PersonAddress?.State,
                } : null
            };
        }

        /// <summary>
        /// Updates the person address.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="city">The city.</param>
        /// <param name="country">The country.</param>
        /// <param name="street">The street.</param>
        /// <param name="zipcode">The zipcode.</param>
        /// <param name="state">The state.</param>
        public async Task UpdatePersonAddress(Guid personId, string city, string country, string street, string zipcode, string state)
        {
            var person = await _personRepository.GetPerson(personId);

            if (person == null) return; // throw person not found exception

            person.ChangePersonAddress(street, country, zipcode, city, state);

             await _personRepository.SavePersonAsync(person);
        }
    }
}
