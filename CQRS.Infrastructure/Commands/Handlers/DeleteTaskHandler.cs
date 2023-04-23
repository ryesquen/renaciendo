using CQRS.Infrastructure.Persistences.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Commands.Handlers
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly CQRSDbContext _context;
        public DeleteTaskHandler(CQRSDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(ti => ti.Id == request.id,cancellationToken);
            if (taskItem is null) { return false; }
            _context.Remove(taskItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
