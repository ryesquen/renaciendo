using AutoMapper;
using BancoAPI.Application.DTOs;
using BancoAPI.Application.Interfaces;
using BancoAPI.Application.Specifications;
using BancoAPI.Application.Wrappers;
using BancoAPI.Domain.Entities;
using MediatR;

namespace BancoAPI.Application.Features.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<PageResponse<List<ClientDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PageResponse<List<ClientDTO>>>
    {
        private readonly IRepositoryAsync<Client> _repository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IRepositoryAsync<Client> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PageResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repository.ListAsync(new PagedClientsSpecification(request.PageSize, request.PageNumber, request.Name!, request.LastName!), cancellationToken);
            var clientsDto = _mapper.Map<List<ClientDTO>>(clients);
            return new PageResponse<List<ClientDTO>>(clientsDto, request.PageNumber, request.PageSize);
        }
    }
}
