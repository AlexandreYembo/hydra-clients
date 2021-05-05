using System;
using Hydra.Core.Abstractions.DomainObjects;
using Hydra.Core.Domain.DomainObjects;
using Hydra.Core.Domain.Validations;

namespace Hydra.Customers.Domain.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        protected Customer(){}//EF Relation
        public Customer(Guid id, string name, string email, string identityNumber)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            IdentityNumber = identityNumber;
            IsRemoved = false;
        }

        public Email Email { get; private set; }
        public string Name { get; private set; }
        public string IdentityNumber { get; private set; }
        public bool IsRemoved { get; private set; }
        public Address Address { get; private set; }

        public void ChangeEmail(string email) => Email = new Email(email);

        public void SetAddress(Address address) => Address = address;

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}