using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Customers.Domain.Models;

namespace Hydra.Customers.Domain.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
         void Add(Customer customer);

         Task<IEnumerable<Customer>> GetAll();

         Task<Customer> GetByIdentityNumber(string identityNumber);
    }
}