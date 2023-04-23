using CQRS.Application.DTOs;
using MediatR;

namespace CQRS.Infrastructure.Commands
{
    public record UpdateTaskCommand(int id, string title, string description, bool isCompleted) : IRequest<TaskItemDto>;
}
