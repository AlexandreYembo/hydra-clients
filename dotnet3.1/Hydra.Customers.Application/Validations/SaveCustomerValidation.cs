using System;
using FluentValidation;
using Hydra.Core.DomainObjects;
using Hydra.Customers.Application.Commands;

namespace Hydra.Customers.Application.Validations
{
    public class SaveCustomerValidation : AbstractValidator<SaveCustomerCommand>
    {
        public SaveCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id invalid");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Customer name is required");

            RuleFor(c => c.IdentityNumber)
                .NotEmpty()
                .WithMessage("Identity Number is required");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .Must(HasValidIEmail)
                .WithMessage("Invalid format of the email");
        }

        protected static bool HasValidIEmail(string email) => Email.IsValid(email);
    }
}