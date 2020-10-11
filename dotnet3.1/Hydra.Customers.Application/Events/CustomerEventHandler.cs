using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Hydra.Customers.Application.Events
{
    /// <summary>
    /// INotificationHandler -> When you release an event
    /// </summary>
    public class CustomerEventHandler : INotificationHandler<CustomerSavedEvent>
    {
        public Task Handle(CustomerSavedEvent notification, CancellationToken cancellationToken)
        {
            //Release the Confirmation Event
            //Implement an infrastructure class that will send the email to the customer
            return Task.CompletedTask;
        }
    }
}