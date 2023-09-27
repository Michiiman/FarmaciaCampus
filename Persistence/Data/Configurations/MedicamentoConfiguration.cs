using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("Medicamento");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Nombre)
            .HasColumnName("Nombre")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(e => e.Precio)
            .HasColumnName("Precio")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(e => e.Stock)
            .HasColumnName("Stock")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(e => e.FechaExpiracion)
            .HasColumnName("FechaExpiracion")
            .HasColumnType("date")
            .IsRequired();

            builder.Property(p => p.TipoMedicamento)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasOne(p => p.Persona)
            .WithMany(p => p.Medicamentos)
            .HasForeignKey(p => p.ProveedorIdFk);
        }
    }
}