using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hydra.Core.Messages;
using Hydra.Customers.Domain.Models;
using MediatR;

namespace Hydra.Customers.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler,
                        IRequestHandler<SaveCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(SaveCustomerCommand message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            //Create the customer object
            var customer = new Customer(message.Id, message.Name, message.Email, message.IdentityNumber);

            //Business Validation


            //Persist on Database
            if(true) //the the customer with the same identity number exists in dabase base
            {
                AddError("There is an existing user with the same identity nunmber.");
                return ValidationResult;
            }

            return message.ValidationResult;
        }
    }
}