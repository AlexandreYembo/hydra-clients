
using System;
using System.Threading.Tasks;
using AutoMapper;
using Hydra.Core.API.Controllers;
using Hydra.Core.API.Identity;
using Hydra.Core.API.User;
using Hydra.Core.Mediator.Abstractions.Mediator;
using Hydra.Customers.API.DTO;
using Hydra.Customers.Application.Commands;
using Hydra.Customers.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Customers.API.Controllers
{
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAspNetUser _user;
        private readonly IMapper _mapper;

        public CustomersController(IMediatorHandler mediatorHandler, ICustomerRepository customerRepository, IAspNetUser user, IMapper mapper)
        {
                _mediatorHandler = mediatorHandler;
                _customerRepository = customerRepository;
                _user = user;
                _mapper = mapper;
        }

        [HttpGet("customers")]
        [ClaimsAuthorize("admin-customer", "read")]
        public async Task<IActionResult> GetCustomers(){
            var result = await _customerRepository.GetAll();
            return CustomResponse(result);
        }

        [HttpGet("customer")]
        [ClaimsAuthorize("customer", "read")]
        public async Task<IActionResult> GetCustomer() =>
            CustomResponse(_mapper.Map<CustomerDTO>(await _customerRepository.GetById(_user.GetUserId())));

        [HttpGet("customer/{identityNumber}")]
        [ClaimsAuthorize("admin-customer", "read")]
        public async Task<IActionResult> GetCustomer(string identityNumber)
        {
            var customer = _mapper.Map<CustomerDTO>(await _customerRepository.GetByIdentityNumber(identityNumber));
            return customer == null ? NotFound() : CustomResponse(customer);
        }

        [HttpPost]
        [Route("customer")]
        [ClaimsAuthorize("admin-customer", "write")] //admin can created a new customer
        public async Task<IActionResult> SaveCustomer(SaveCustomerCommand customer) =>
            CustomResponse(await _mediatorHandler.SendCommand(customer));

        [HttpPut]
        [Route("customer")]
        [ClaimsAuthorize("customer", "write")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand customer)
        {
            customer.Id = _user.GetUserId();
            return CustomResponse(await _mediatorHandler.SendCommand(customer));
        }

        [HttpPut]
        [Route("customer/{id}")]
        [ClaimsAuthorize("admin-customer", "write")]
        public async Task<IActionResult> UpdateCustomerById(UpdateCustomerCommand customer, Guid id)
        {
            customer.Id = id;
            return CustomResponse(await _mediatorHandler.SendCommand(customer));
        }
        
        [HttpGet("customer/address")]
        [ClaimsAuthorize("customer", "read")]
        public async Task<IActionResult> GetAddress()
        {
            var address = _mapper.Map<AddressDTO>(await _customerRepository.GetAddressByCustomerId(_user.GetUserId()));
            return address == null ? NotFound() : CustomResponse(address);
        }

        [HttpPost("customer/address")]
        [ClaimsAuthorize("customer", "write")]
        public async Task<IActionResult> SaveAddress(SaveAddressCommand address)
        {
            address.CustomerId = _user.GetUserId();
            return CustomResponse(await _mediatorHandler.SendCommand(address));
        }
    }
}