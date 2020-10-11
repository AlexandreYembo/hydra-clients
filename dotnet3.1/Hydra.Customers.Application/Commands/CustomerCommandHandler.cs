using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hydra.Core.Messages;
using Hydra.Customers.Domain.Models;
using Hydra.Customers.Domain.Repository;
using MediatR;

namespace Hydra.Customers.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler,
                                        IRequestHandler<SaveCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _repository;

        public CustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(SaveCustomerCommand message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            //Create the customer object
            var customer = new Customer(message.Id, message.Name, message.Email, message.IdentityNumber);

            //Business Validation
            var existingCustomer = await _repository.GetByIdentityNumber(customer.IdentityNumber);

            //Persist on Database
            if(existingCustomer != null) //the the customer with the same identity number exists in dabase base
            {
                AddError("There is an existing user with the same identity nunmber.");
                return ValidationResult;
            }

            _repository.Add(customer);

            return await Save(_repository.UnitOfWork);
        }
    }
}