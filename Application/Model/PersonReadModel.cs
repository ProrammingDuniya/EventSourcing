namespace Application.Model
{
    public class PersonReadModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonId { get; set; }

        public AddressModel Address { get; set; }
    }
}
