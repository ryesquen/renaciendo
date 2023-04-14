using Microsoft.EntityFrameworkCore;
using RenaciendoWebAPI.Models;

namespace RenaciendoWebAPI.Datos
{
    public class RenacerContext : DbContext
    {
        public RenacerContext(DbContextOptions<RenacerContext> options) : base(options) { }
        public DbSet<Renacer> Renaceres { get; set; }
    }
}