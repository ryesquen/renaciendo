using Microsoft.EntityFrameworkCore;

namespace EF7Masivo.Models;

public partial class MasivoContext : DbContext
{
    public MasivoContext()
    {
        
    }
    public MasivoContext(DbContextOptions<MasivoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Masivo> Masivo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 14333;Database=POS;TrustServerCertificate=true;User=sa;Password=_RE41Nz000;MultipleActiveResultSets=true").LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Masivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__masivo__3214EC070194E18D");

            entity.Property(e => e.AnotherDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}