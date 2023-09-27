using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class FarmaciaContext : DbContext
{
    public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options)
    { }
    //Main

    public DbSet<TipoDocumento> TiposDocumentos { get; set; }
    public DbSet<Telefono> Telefonos { get; set; }
    public DbSet<TipoPersona> TiposPersonas { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Receta> Recetas { get; set; }
    public DbSet<MedicamentoComprado> MedicamentosComprados { get; set; }
    public DbSet<FacturaVenta> FacturasVentas { get; set; }
    public DbSet<FacturaVenta> PacientesVentas { get; set; }
    public DbSet<FacturaVenta> EmpleadosVentas { get; set; }
    public DbSet<MedicamentoReceta> MedicamentosRecetas { get; set; }
    public DbSet<MedicamentoVendido> MedicamentoVendidos { get; set; }
    public DbSet<Receta> Doctores { get; set; }
    public DbSet<Receta> Pacientes { get; set; }

    //JWT
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Rols { get; set; }
    public DbSet<UserRol> UserRols { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}



