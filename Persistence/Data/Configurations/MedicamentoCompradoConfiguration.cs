using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicamentoCompradoConfiguration : IEntityTypeConfiguration<MedicamentoComprado>
    {
        public void Configure(EntityTypeBuilder<MedicamentoComprado> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("MedicamentoComprado");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.CantidadComprada)
            .HasColumnName("CantidadComprada")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(e => e.PrecioCompra)
            .HasColumnName("PrecioCompra")
            .HasColumnType("int")
            .IsRequired();

            builder.HasOne(p => p.Compra)
            .WithMany(p => p.MedicamentosComprados)
            .HasForeignKey(p => p.CompraIdFk);

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.MedicamentosComprados)
            .HasForeignKey(p => p.MedicamentoIdFk);
        }
    }
}