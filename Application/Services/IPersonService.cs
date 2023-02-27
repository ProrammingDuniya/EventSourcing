using Application.Model;
using Core.ValueObjects;

namespace Application.Services
{

    /// <summary>
    /// IPersonService
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Creates the person.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        Task<PersonId> CreatePerson(string firstName, string lastName);

        /// <summary>
        /// Gets the person data.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        Task<PersonReadModel> GetPersonData(Guid personId);

        /// <summary>
        /// Updates the person address.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="city">The city.</param>
        /// <param name="country">The country.</param>
        /// <param name="street">The street.</param>
        /// <param name="zipcode">The zipcode.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        Task UpdatePersonAddress(Guid personId, string city, string country, string street, string zipcode, string state);
    }
}
