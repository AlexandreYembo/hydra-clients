
using System;
using System.Threading.Tasks;
using Hydra.Core.Communication.Mediator;
using Hydra.Customers.Application.Commands;
using Hydra.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Customers.API.Controllers
{
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomersController(IMediatorHandler mediatorHandler)
        {
                _mediatorHandler = mediatorHandler;
        }

       [HttpGet("customers")]
       public async Task<IActionResult> GetAction(){
            var result = await _mediatorHandler.SendCommand(new SaveCustomerCommand(Guid.NewGuid(), "Alexandre", "alexandre@gmail.com", "QA2432222"));
            return CustomResponse(result);
       }
    }
}