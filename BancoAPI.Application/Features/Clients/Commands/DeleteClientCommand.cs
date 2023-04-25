using BancoAPI.Application.Interfaces;
using BancoAPI.Application.Wrappers;
using BancoAPI.Domain.Entities;
using MediatR;

namespace BancoAPI.Application.Features.Clients.Commands
{
    public class DeleteClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;

        public DeleteClientCommandHandler(IRepositoryAsync<Client> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<Response<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (client is null) { throw new KeyNotFoundException($"No se encuentra el cliente con Id {request.Id}"); }
            else
            {
                await _repositoryAsync.DeleteAsync(client, cancellationToken);
                return new Response<int>(client.Id);
            }

        }
    }
}
