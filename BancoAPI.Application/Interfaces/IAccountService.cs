using BancoAPI.Application.DTOs.Users;
using BancoAPI.Application.Wrappers;

namespace BancoAPI.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
    }
}