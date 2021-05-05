using System;
using Hydra.Core.Domain.DomainObjects;

namespace Hydra.Customers.Domain.Models
{
    public class Address: Entity
    {
        protected Address(){ }
        public Address(string street, string number, string city, string state, string postCode, string country, Guid customerId)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            PostCode = postCode;
            Country = country;
            CustomerID = customerId;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostCode { get; private set; }
        public string Country { get; private set; }

        public Guid CustomerID { get; private set; }

        //EF Relation
        public Customer Customer { get; protected set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}