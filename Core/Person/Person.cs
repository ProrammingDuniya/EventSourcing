using Core.DomainEvents;
using Core.Person.DomainEvents;
using Core.ValueObjects;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Core.Person
{
    /// <summary>
    /// Person
    /// </summary>
    public class Person : BaseEntity<PersonId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address PersonAddress { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="Person"/> class from being created.
        /// </summary>
        private Person()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public Person(IEnumerable<IDomainEvent> events) : base(events)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        private Person(string firstName, string lastName)
        {
            var personId = new PersonId();
            var personCreatedEvent =  new PersonCreated(personId, firstName, lastName);

            this.AddEvent(personCreatedEvent);
        }

        /// <summary>
        /// Changes the person address.
        /// </summary>
        /// <param name="street">The street.</param>
        /// <param name="country">The country.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        public void ChangePersonAddress(string street, string country, string zipCode, string city, string state)
        {
            var personAddressEvent = new AddressChanged(city, country, zipCode, street, state);

            this.AddEvent(personAddressEvent);
        }

        /// <summary>
        /// Applies the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        public void Apply(PersonCreated @event)
        {
            Id = (PersonId)@event.PersonId;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        /// <summary>
        /// Applies the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        public void Apply(AddressChanged @event)
        {
            PersonAddress = new Address(@event.City, @event.Country, @event.Street, @event.ZipCode, @event.State);
        }

        /// <summary>
        /// Creates the new person.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public static Person CreateNewPerson(string firstName, string lastName) => new(firstName, lastName);
    }
}
