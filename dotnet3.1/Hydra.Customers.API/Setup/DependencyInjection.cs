using FluentValidation.Results;
using Hydra.Core.Communication.Mediator;
using Hydra.Customers.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Customers.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<SaveCustomerCommand, ValidationResult>, CustomerCommandHandler>();
        }
    }
}