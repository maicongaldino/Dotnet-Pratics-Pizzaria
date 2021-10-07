using AutoMapper;
using Pizzaria.Application.Dtos;
using Pizzaria.Domain;

namespace Pizzaria.Application.Helpers
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<Pizza, PizzaDto>().ReverseMap();
        }
    }
}