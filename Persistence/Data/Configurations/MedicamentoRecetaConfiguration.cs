using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicamentosRecetaConfiguration : IEntityTypeConfiguration<MedicamentoReceta>
    {
        public void Configure(EntityTypeBuilder<MedicamentoReceta> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("MedicamentosReceta");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.MedicamentosRecetas)
            .HasForeignKey(p => p.MedicamentosIdfk);

            builder.HasOne(p => p.Receta)
            .WithMany(p => p.MedicamentosRecetas)
            .HasForeignKey(p => p.RecetaIdFk);
        }
    }
}