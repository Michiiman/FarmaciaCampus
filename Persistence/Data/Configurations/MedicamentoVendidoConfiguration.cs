using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicamentoVendidoConfiguration : IEntityTypeConfiguration<MedicamentoVendido>
    {
        public void Configure(EntityTypeBuilder<MedicamentoVendido> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("MedicamentoVendido");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.CantidadVendida)
            .HasColumnName("Cantidad")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(e => e.Descripcion)
            .HasColumnName("Descripcion")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.HasOne(p => p.FacturaVenta)
            .WithMany(p => p.MedicamentosVendidos)
            .HasForeignKey(p => p.FacturaVentaIdFk);

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.MedicamentosVendidos)
            .HasForeignKey(p => p.MedicamentoIdFk);
        }
    }
}