namespace Hydra.Customers.API.DTO
{
    public class AddressDTO
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostCode { get; private set; }
        public string Country { get; private set; }

    }
}