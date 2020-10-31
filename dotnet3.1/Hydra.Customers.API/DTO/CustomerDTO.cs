namespace Hydra.Customers.API.DTO
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
           
        }

        public string Email { get; private set; }
        public string Name { get; private set; }
        public string IdentityNumber { get; private set; }
        public bool IsRemoved { get; private set; }
        public AddressDTO Address { get; private set; }    
    }
}