using CQRS.Application.DTOs;
using CQRS.Domain.Entities;
using CQRS.Infrastructure.Persistences.Contexts;
using MediatR;

namespace CQRS.Infrastructure.Commands.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
    {
        private readonly CQRSDbContext _context;
        public CreateTaskHandler(CQRSDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new TaskItem
            {
                Description = request.description,
                Title = request.title
            };
            await _context.AddAsync(taskItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted
            };
        }
    }
}