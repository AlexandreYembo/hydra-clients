using FluentValidation.Results;
using Hydra.Core.Communication.Mediator;
using Hydra.Customers.API.Services;
using Hydra.Customers.Application.Commands;
using Hydra.Customers.Application.Events;
using Hydra.Customers.Domain.Repository;
using Hydra.Customers.Infrastructure.Data;
using Hydra.Customers.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Customers.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //DI for Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //DI for commands
            services.AddScoped<IRequestHandler<SaveCustomerCommand, ValidationResult>, CustomerCommandHandler>();

            //DI for Events
            services.AddScoped<INotificationHandler<CustomerSavedEvent>, CustomerEventHandler>();

            //DI for Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();
        }
    }
}