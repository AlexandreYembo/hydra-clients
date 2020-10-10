using System;
using Hydra.Core.DomainObjects;
using Hydra.Customers.Domain.ValueObjects;

namespace Hydra.Customers.Domain.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        protected Customer(){}//EF Relation
        public Customer(Guid id, string name, string email, string identityCard, bool isRemoved)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            IdentityCard = identityCard;
            IsRemoved = false;
        }

        public Email Email { get; private set; }
        public string Name { get; private set; }
        public string IdentityCard { get; private set; }
        public bool IsRemoved { get; private set; }
        public Address Address { get; private set; }

        public void ChangeEmail(string email) => Email = new Email(email);

        public void SetAddress(Address address) => Address = address;
     }
}