using AutoMapper;
using BancoAPI.Application.Features.Clients.Commands;
using BancoAPI.Domain.Entities;

namespace BancoAPI.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region
            CreateMap<CreateClientCommand, Client>();
            #endregion
        }
    }
}
