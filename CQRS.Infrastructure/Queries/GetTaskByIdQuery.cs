using CQRS.Application.DTOs;
using MediatR;

namespace CQRS.Infrastructure.Queries
{
    public record GetTaskByIdQuery(int id) : IRequest<TaskItemDto>;
}
