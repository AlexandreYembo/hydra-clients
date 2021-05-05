using System;
using FluentValidation;
using Hydra.Core.Mediator.Messages;

namespace Hydra.Customers.Application.Commands
{
    public class SaveAddressCommand : Command
    {
        public SaveAddressCommand()
        {
        }
        public SaveAddressCommand(string street, string number, string city, string state, string postCode, string country)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            PostCode = postCode;
            Country = country;
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Guid CustomerId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddressCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class AddressCommandValidation : AbstractValidator<SaveAddressCommand>
    {
        private string GetMessage(string field) => $"{field} is mandatory";
        public AddressCommandValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty()
                .WithMessage(GetMessage("Street"));

            RuleFor(c => c.Number)
                .NotEmpty()
                .WithMessage(GetMessage("Number"));
            
            RuleFor(c => c.PostCode)
                .NotEmpty()
                .WithMessage(GetMessage("PostCode"));
            
            RuleFor(c => c.State)
                .NotEmpty()
                .WithMessage(GetMessage("State"));
            
            RuleFor(c => c.City)
                .NotEmpty()
                .WithMessage(GetMessage("City"));

            RuleFor(c => c.Country)
                .NotEmpty()
                .WithMessage(GetMessage("Country"));
        }
    }
}