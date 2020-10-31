using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hydra.Core.Messages;
using Hydra.Customers.Application.Events;
using Hydra.Customers.Domain.Models;
using Hydra.Customers.Domain.Repository;
using MediatR;

namespace Hydra.Customers.Application.Commands
{
    //IRequestHandler -> When you send anything
    public class CustomerCommandHandler : CommandHandler,
                                        IRequestHandler<SaveCustomerCommand, ValidationResult>,
                                        IRequestHandler<UpdateCustomerCommand, ValidationResult>,
                                        IRequestHandler<SaveAddressCommand, ValidationResult>
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

            customer.AddEvent(new CustomerSavedEvent(message.Id, message.Name, message.Email, message.IdentityNumber));

            return await Save(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(SaveAddressCommand message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            var address = new Address(message.Street, message.Number, message.City, message.State, message.PostCode, message.Country, message.CustomerId);

            _repository.SaveAddress(address);

            return await Save(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if(!message.IsValid()) return message.ValidationResult;

            var existingCustomer = await _repository.GetById(message.Id);

            var customer = new Customer(message.Id, message.Name, message.Email, existingCustomer.IdentityNumber);

            if(customer == null){
                AddError("Customer not found");
                return message.ValidationResult;
            }

            _repository.Add(customer);

            customer.AddEvent(new CustomerSavedEvent(message.Id, message.Name, message.Email, existingCustomer.IdentityNumber));

            return await Save(_repository.UnitOfWork);
        }
    }
}