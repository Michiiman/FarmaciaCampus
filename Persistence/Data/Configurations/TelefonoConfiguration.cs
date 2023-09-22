using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class TelefonoConfiguration : IEntityTypeConfiguration<Telefono>
    {
        public void Configure(EntityTypeBuilder<Telefono> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("Telefono");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Numero)
            .HasColumnName("Numero")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(e => e.TipoTelefono)
            .HasColumnName("TipoTelefono")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

            builder.HasOne(p => p.Persona)
            .WithMany(p => p.Telefonos)
            .HasForeignKey(p => p.PersonaIdFk);
        }
    }
}