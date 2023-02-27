using Core.DomainEvents;

namespace Core.Person.DomainEvents
{
    public record PersonCreated(
            Guid PersonId,
            string FirstName,
            string LastName) : DomainEvent;
}
