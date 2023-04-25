using AutoMapper;
using BancoAPI.Application.DTOs;
using BancoAPI.Application.Interfaces;
using BancoAPI.Application.Specifications;
using BancoAPI.Application.Wrappers;
using BancoAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

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
        private readonly IDistributedCache _distributedCache;

        public GetAllClientsQueryHandler(IRepositoryAsync<Client> repository, IMapper mapper, IDistributedCache distributedCache)
        {
            _repository = repository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }
        public async Task<PageResponse<List<ClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"listadoClientes_{request.PageSize}_{request.PageNumber}_{request.Name}_{request.LastName}";
            string serializedListadoClientes = string.Empty;
            var listadoClientes = new List<Client>();
            var redisListadoClientes = await _distributedCache.GetAsync(cacheKey, cancellationToken).ConfigureAwait(false);
            if (redisListadoClientes is not null)
            {
                serializedListadoClientes = Encoding.UTF8.GetString(redisListadoClientes);
                listadoClientes = JsonSerializer.Deserialize<List<Client>>(serializedListadoClientes);
            }
            else
            {
                listadoClientes = await _repository.ListAsync(new PagedClientsSpecification(request.PageSize, request.PageNumber, request.Name!, request.LastName!), cancellationToken);
                serializedListadoClientes = JsonSerializer.Serialize(listadoClientes);
                redisListadoClientes = Encoding.UTF8.GetBytes(serializedListadoClientes);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisListadoClientes, options);
            }
            var clientsDto = _mapper.Map<List<ClientDTO>>(listadoClientes);
            return new PageResponse<List<ClientDTO>>(clientsDto, request.PageNumber, request.PageSize);
        }
    }
}
