using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("Persona");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Nombre)
            .HasColumnName("Nombre")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(e => e.Direccion)
            .HasColumnName("Direccion")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.HasOne(p => p.TipoDeDocumento)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.TipoDeDocumentoIdFk);

            builder.HasOne(p => p.TipoPersona)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.TipoPersonaIdFk);
        }
    }
}