using MediatR;

namespace CQRS.Infrastructure.Commands
{
    public record DeleteTaskCommand(int id) : IRequest<bool>;
}
