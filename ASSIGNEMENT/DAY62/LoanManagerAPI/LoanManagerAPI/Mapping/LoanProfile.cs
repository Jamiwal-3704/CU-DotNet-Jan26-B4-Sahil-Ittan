using AutoMapper;
using LoanManagerAPI.DTOs;
using LoanManagerAPI.Models;

namespace LoanManagerAPI.Mapping
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<Loan, LoanReadDTO>();
            CreateMap<LoanCreateDTO, Loan>();
            CreateMap<LoanUpdateDTO, Loan>();
        }
    }
}
