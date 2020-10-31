using AutoMapper;
using Hydra.Customers.API.DTO;
using Hydra.Customers.Domain.Models;

namespace Hydra.Customers.API.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(m => m.Email, m => m.MapFrom(mf => mf.Email.Value));

            CreateMap<Address, AddressDTO>();
        }
    }
}