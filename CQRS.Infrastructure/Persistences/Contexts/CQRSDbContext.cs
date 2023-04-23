using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Persistences.Contexts
{
    public class CQRSDbContext : DbContext
    {
        public CQRSDbContext(DbContextOptions<CQRSDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}