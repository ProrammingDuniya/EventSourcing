using Core.DomainEvents;

namespace Core.Person.DomainEvents
{
    public record AddressChanged(string City,
            string Country,
            string ZipCode,
            string Street,
            string State) : DomainEvent;
}

