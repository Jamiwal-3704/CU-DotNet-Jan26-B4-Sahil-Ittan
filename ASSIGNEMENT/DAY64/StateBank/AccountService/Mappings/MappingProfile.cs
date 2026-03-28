using AutoMapper;
using StateBank.DTOs;
using StateBank.Models;

namespace StateBank.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<CreateAccountDto, Account>();
        }
    }
}
