using System;
using Hydra.Core.Messages;

namespace Hydra.Customers.Application.Events
{
    public class CustomerSavedEvent : Event
    {
        public CustomerSavedEvent(Guid id, string name, string email, string identityNumber)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            IdentityNumber = identityNumber;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string IdentityNumber { get; private set; }
    }
}