using Microsoft.EntityFrameworkCore;
using RenaciendoWebAPI.Models;

namespace RenaciendoWebAPI.Datos
{
    public class RenacerContext : DbContext
    {
        public RenacerContext(DbContextOptions<RenacerContext> options) : base(options) { }
        public DbSet<Renacer> Renaceres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Renacer>().HasData(
                new Renacer { RenacerId = 1, Nombre = "Manzanita", Amenidad = "Chiste de 🐈‍", Detalle = "Siempre", Dimension = 123, FechaCreacion = DateTime.Now, FechaActualizacion = DateTime.Now },
                new Renacer { RenacerId = 2, Nombre = "Mau", Amenidad = "Chiste de 🍏", Detalle = "Constante", Dimension = 321, FechaCreacion = DateTime.Now, FechaActualizacion = DateTime.Now }
                );
        }
    }
}