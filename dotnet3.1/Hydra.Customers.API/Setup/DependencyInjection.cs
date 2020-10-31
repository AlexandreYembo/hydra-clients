using FluentValidation.Results;
using Hydra.Core.Communication.Mediator;
using Hydra.Customers.API.Services;
using Hydra.Customers.Application.Commands;
using Hydra.Customers.Application.Events;
using Hydra.Customers.Domain.Repository;
using Hydra.Customers.Infrastructure.Data;
using Hydra.Customers.Infrastructure.Repositories;
using Hydra.WebAPI.Core.User;
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
            services.AddScoped<IRequestHandler<SaveCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<SaveAddressCommand, ValidationResult>, CustomerCommandHandler>();

            //DI for Events
            services.AddScoped<INotificationHandler<CustomerSavedEvent>, CustomerEventHandler>();

            //DI for Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();
        }
    }
}