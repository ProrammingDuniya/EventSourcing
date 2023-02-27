using Core.Person;
using Core.ValueObjects;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// IPersonRepository
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Saves the person asynchronous.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        Task<PersonId> SavePersonAsync(Person person);

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        Task<Person> GetPerson(Guid personId);
    }
}
