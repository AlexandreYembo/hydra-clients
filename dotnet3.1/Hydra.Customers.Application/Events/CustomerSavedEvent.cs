using System;
using Hydra.Core.Mediator.Messages;

namespace Hydra.Customers.Application.Events
{
    public class CustomerSavedEvent : Event
    {
        public CustomerSavedEvent(Guid id, string name, string email, string identityNumber)
        {
            AggregateId = id;
            Name = name;
            Email = email;
            IdentityNumber = identityNumber;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
    }
}