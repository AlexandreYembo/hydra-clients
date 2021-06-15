using FluentValidation.Results;
using Hydra.Core.API.User;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Core.Mediator.Communication;
using Hydra.Core.Mediator.Messages;
using Hydra.Customers.Application.Commands;
using Hydra.Customers.Application.Events;
using Hydra.Customers.Domain.Repository;
using Hydra.Customers.Infrastructure.Data;
using Hydra.Customers.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Customers.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
             //API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            //DI for Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //DI for commands
            services.AddScoped<IRequestHandler<SaveCustomerCommand, CommandResult<ValidationResult>>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, CommandResult<ValidationResult>>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<SaveAddressCommand, CommandResult<ValidationResult>>, CustomerCommandHandler>();

            //DI for Events
            services.AddScoped<INotificationHandler<CustomerSavedEvent>, CustomerEventHandler>();

            //DI for Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();
        }
    }
}