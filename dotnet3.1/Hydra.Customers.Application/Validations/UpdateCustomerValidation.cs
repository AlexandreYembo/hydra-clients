using System;
using FluentValidation;
using Hydra.Core.Domain.Validations;
using Hydra.Customers.Application.Commands;

namespace Hydra.Customers.Application.Validations
{
    public class UpdateCustomerValidation : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id invalid");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Customer name is required");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .Must(HasValidIEmail)
                .WithMessage("Invalid format of the email");
        }

        protected static bool HasValidIEmail(string email) => Email.IsValid(email);
    }
}