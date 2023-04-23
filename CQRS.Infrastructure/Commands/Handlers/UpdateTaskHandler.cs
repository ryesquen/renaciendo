using CQRS.Application.DTOs;
using CQRS.Infrastructure.Persistences.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Commands.Handlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskItemDto?>
    {
        private readonly CQRSDbContext _context;
        public UpdateTaskHandler(CQRSDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItemDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(ti => ti.Id == request.id, cancellationToken);
            if (taskItem is null) { return null; }
            taskItem.Title = request.title;
            taskItem.Description = request.description;
            taskItem.IsCompleted = request.isCompleted;
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
