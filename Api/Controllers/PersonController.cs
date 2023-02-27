using Application.Model;
using Application.Services;
using Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Person Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="personService">The person service.</param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Create Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> CreatePerson([FromBody] PersonCreateModel person)
        {
            var insertedPersonId = await _personService.CreatePerson(person.FirstName, person.LastName);
            return new { PersonId = insertedPersonId.ToString() };
        }

        /// <summary>
        /// Changes the person address.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="address">The address.</param>
        [HttpPost]
        [Route("UpdateAddress")]
        public async Task ChangePersonAddress(
          [FromQuery] Guid personId,
           [FromBody] AddressModel address)
        {
            await _personService.UpdatePersonAddress(new PersonId(personId),
                address.City, address.Country, address.Street, address.ZipCode, address.State);
            Ok();
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PersonReadModel> GetPerson([FromQuery] Guid personId)
        {
            return await _personService.GetPersonData(personId);
        }
    }
}
