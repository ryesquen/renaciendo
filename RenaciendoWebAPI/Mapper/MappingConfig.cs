using AutoMapper;
using RenaciendoWebAPI.Models;
using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Renacer, RenacerDTO>().ReverseMap();
        }
    }
}