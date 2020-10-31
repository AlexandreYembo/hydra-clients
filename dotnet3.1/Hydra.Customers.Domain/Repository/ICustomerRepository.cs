using System;
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
         Task<Customer> GetById(Guid id);

         Task<Customer> GetByIdentityNumber(string identityNumber);
        Task<Address> GetAddressByCustomerId(Guid customerId);

        void SaveAddress(Address address);
    }
}