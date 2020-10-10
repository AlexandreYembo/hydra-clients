using System;
using Hydra.Core.DomainObjects;

namespace Hydra.Customers.Domain.Models
{
    public class Address: Entity
    {
        public Address(string street, string number, string city, string state, string postCode, string country)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            PostCode = postCode;
            Country = country;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostCode { get; private set; }
        public string Country { get; private set; }

        public Guid CustomerID { get; private set; }

        //EF Relation
        public Customer Customer { get; private set; }
    }
}