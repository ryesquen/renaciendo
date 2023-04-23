using CQRS.Application.DTOs;
using MediatR;

namespace CQRS.Infrastructure.Queries
{
    public record GetAllTaskQuery() : IRequest<IEnumerable<TaskItemDto>>;
}
