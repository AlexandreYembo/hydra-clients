using System;
using Hydra.Core.Messages;

namespace Hydra.Customers.Application.Commands
{
    /// <summary>
    /// This class is a transport that represents only one purpose
    /// </summary>
    public class SaveCustomerCommand : Command
    {
        public SaveCustomerCommand(Guid id, string name, string email, string identityNumber)
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