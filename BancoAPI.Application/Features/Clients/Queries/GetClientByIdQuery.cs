using AutoMapper;
using BancoAPI.Application.DTOs;
using BancoAPI.Application.Interfaces;
using BancoAPI.Application.Wrappers;
using BancoAPI.Domain.Entities;
using MediatR;

namespace BancoAPI.Application.Features.Clients.Queries
{
    public class GetClientByIdQuery : IRequest<Response<ClientDTO>>
    {
        public int Id { get; set; }
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Response<ClientDTO>>
    {
        private readonly IRepositoryAsync<Client> _repository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IRepositoryAsync<Client> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return client is null
                ? throw new KeyNotFoundException($"No se encuentra el cliente con Id {request.Id}")
                : new Response<ClientDTO>(_mapper.Map<ClientDTO>(client));
        }
    }
}
