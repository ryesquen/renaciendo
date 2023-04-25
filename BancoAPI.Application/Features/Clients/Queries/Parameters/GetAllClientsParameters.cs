using BancoAPI.Application.Parameters;

namespace BancoAPI.Application.Features.Clients.Queries.Parameters
{
    public class GetAllClientsParameters : RequestParameter
    {
        public string? Name{ get; set; }
        public string? LastName { get; set; }
    }
}
