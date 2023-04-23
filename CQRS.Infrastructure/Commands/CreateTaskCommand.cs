using CQRS.Application.DTOs;
using MediatR;

namespace CQRS.Infrastructure.Commands
{
    public record CreateTaskCommand(string title, string description) : IRequest<TaskItemDto>;
}
