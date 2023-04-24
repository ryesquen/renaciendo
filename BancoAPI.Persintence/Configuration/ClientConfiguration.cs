using BancoAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoAPI.Persistence.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(x => x.Id);
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(80);
            builder.Property(p=>p.LastName).IsRequired().HasMaxLength(80);
            builder.Property(p=>p.Birthdate).IsRequired();
            builder.Property(p=>p.PhoneNumber).IsRequired().HasMaxLength(9);
            builder.Property(p=>p.Email).HasMaxLength(100);
            builder.Property(p=>p.Address).HasMaxLength(120);
            builder.Property(p => p.Edad);
            builder.Property(p=>p.CreatedBy).HasMaxLength(30);
            builder.Property(p=>p.LastModifiedBy).HasMaxLength(30);
        }
    }
}