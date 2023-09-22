using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class FacturaVentaConfiguration : IEntityTypeConfiguration<FacturaVenta>
    {
        public void Configure(EntityTypeBuilder<FacturaVenta> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("FacturaVenta");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.FechaFactura)
            .HasColumnName("FechaFactura")
            .HasColumnType("date")
            .IsRequired();

            builder.Property(e => e.PrecioTotal)
            .HasColumnName("PrecioTotal")
            .HasColumnType("int")
            .IsRequired();

            builder.HasOne(p => p.Persona)
            .WithMany(p => p.FacturasVentas)
            .HasForeignKey(p => p.PacienteIdFk);

            builder.HasOne(p => p.Persona)
            .WithMany(p => p.FacturasVentas)
            .HasForeignKey(p => p.EmpleadoIdFk);

            builder.HasOne(p => p.Receta)
            .WithOne(p => p.FacturaVenta)
            .HasForeignKey<FacturaVenta>(p => p.RecetaIdFk);
        }
    }
}