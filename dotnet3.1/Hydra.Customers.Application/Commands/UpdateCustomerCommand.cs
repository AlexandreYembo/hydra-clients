using System;
using Hydra.Core.Mediator.Messages;
using Hydra.Customers.Application.Validations;

namespace Hydra.Customers.Application.Commands
{
    public class UpdateCustomerCommand : Command
    {

        public UpdateCustomerCommand() { }



        public UpdateCustomerCommand(Guid id, string name, string email)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}