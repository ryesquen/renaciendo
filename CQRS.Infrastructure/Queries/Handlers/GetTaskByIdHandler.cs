using CQRS.Application.DTOs;
using CQRS.Infrastructure.Persistences.Contexts;
using MediatR;

namespace CQRS.Infrastructure.Queries.Handlers
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto?>
    {
        private readonly CQRSDbContext _context;
        public GetTaskByIdHandler(CQRSDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItemDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItems.FindAsync(new object[] { request.id }, cancellationToken);
            if (taskItem is null) { return null; }
            return new TaskItemDto
            {
                Id = taskItem!.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted
            };
        }
    }
}
