using System;
using Hydra.Core.Mediator.Messages;
using Hydra.Customers.Application.Validations;

namespace Hydra.Customers.Application.Commands
{
    /// <summary>
    /// This class is a transport that represents only one purpose
    /// </summary>
    public class SaveCustomerCommand : Command
    {
        public SaveCustomerCommand(){ }
        
        public SaveCustomerCommand(Guid id, string name, string email, string identityNumber)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            IdentityNumber = identityNumber;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new SaveCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}