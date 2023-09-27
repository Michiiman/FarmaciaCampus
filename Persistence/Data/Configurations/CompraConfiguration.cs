using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ComprasConfiguration : IEntityTypeConfiguration<Compra>
{
    public void Configure(EntityTypeBuilder<Compra> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad Marca
        // utilizando el objeto 'builder'.
        builder.ToTable("compra");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.FechaCompra)
        .HasColumnName("FechaCompra")
        .HasColumnType("date")
        .IsRequired();

        builder.HasOne(p => p.Persona)
        .WithMany(p => p.Compras)
        .HasForeignKey(p => p.ProveedorIdFk);
    }
}