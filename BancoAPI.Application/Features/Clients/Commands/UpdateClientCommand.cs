using AutoMapper;
using BancoAPI.Application.Interfaces;
using BancoAPI.Application.Wrappers;
using BancoAPI.Domain.Entities;
using MediatR;

namespace BancoAPI.Application.Features.Clients.Commands
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (record is null) { throw new KeyNotFoundException($"No se encuentra el cliente con Id {request.Id}"); }
            else
            {
                record.Name = request.Name;
                record.LastName = request.LastName;
                record.Birthdate = request.Birthdate;
                record.PhoneNumber = request.PhoneNumber;
                record.Email = request.Email;
                record.Address = request.Address;
                await _repositoryAsync.UpdateAsync(record, cancellationToken);
                return new Response<int>(record.Id);
            }
        }
    }
}
