using CQRS.Application.DTOs;
using CQRS.Infrastructure.Persistences.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Queries.Handlers
{
    public class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery, IEnumerable<TaskItemDto>?>
    {
        private readonly CQRSDbContext _context;
        public GetAllTaskHandler(CQRSDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItemDto>?> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            var listTaskItems = await _context.TaskItems.ToListAsync(cancellationToken);
            if (listTaskItems is null) { return null; }
            return listTaskItems.Select(
                taskItem => new TaskItemDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    Description = taskItem.Description,
                    IsCompleted = taskItem.IsCompleted
                });
        }
    }
}