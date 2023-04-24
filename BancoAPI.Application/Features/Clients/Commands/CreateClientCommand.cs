using BancoAPI.Application.Wrappers;
using MediatR;

namespace BancoAPI.Application.Features.Clients.Commands
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        public Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
