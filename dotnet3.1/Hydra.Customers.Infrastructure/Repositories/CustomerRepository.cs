using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Customers.Domain.Models;
using Hydra.Customers.Domain.Repository;
using Hydra.Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Customers.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext _context;

        public CustomerRepository(CustomersContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Customer customer) => _context.Customers.Add(customer);

        public async Task<IEnumerable<Customer>> GetAll() => 
            await _context.Customers.AsNoTracking()
                                    .ToListAsync();
        
        public async  Task<Customer> GetById(Guid id)=> 
            await _context.Customers.AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Customer> GetByIdentityNumber(string identityNumber) =>
            await _context.Customers.FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber);

        public async Task<Address> GetAddressByCustomerId(Guid customerId) =>
            await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerID == customerId);

        public void SaveAddress(Address address) =>
            _context.Addresses.Add(address);

        public void Dispose() => _context.Dispose();
    }
}