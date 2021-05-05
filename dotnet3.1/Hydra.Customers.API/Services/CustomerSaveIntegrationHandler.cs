using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Core.Mediator.Integration;
using Hydra.Core.MessageBus;
using Hydra.Customers.Application.Commands;
using Hydra.User.Integration.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hydra.Customers.API.Services
{
    /// <summary>
    /// Work as background service
    /// </summary>
    public class CustomerSaveIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider; //Use this to inject the context scope in a singleton instance.

        public CustomerSaveIntegrationHandler(IServiceProvider serviceProvider, IMessageBus message)
        {
            _serviceProvider = serviceProvider;
            _messageBus = message;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            _messageBus.AdvancedBus.Connected += OnConnect;

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> SaveCustomer(UserSaveIntegrationEvent messge){
            var customerCommand = new SaveCustomerCommand(messge.Id, messge.Name, messge.Email, messge.IdentityNumber);
            ValidationResult result;
            using(var scope = _serviceProvider.CreateScope()) // Create scope inside the singleton (Lifecicle scope)
            {
                //Basically service locator is used When it is outside the context or when the class cannot pass arguments through the constructor
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                result = await mediator.SendCommand(customerCommand);
            }

            return new ResponseMessage(result);
        }

        /// <summary>
        /// It will renew the subscription when the application will be abble to connect with RabbitMQ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnConnect(object sender, EventArgs e) => SetResponder();

        private void SetResponder()
        {
              _messageBus.RespondAsync<UserSaveIntegrationEvent, ResponseMessage>(async request =>
                await SaveCustomer(request));
        }
    }
}