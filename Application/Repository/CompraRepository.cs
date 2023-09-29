using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class CompraRepository : GenericRepository<Compra>, ICompra
{
    protected readonly FarmaciaContext _context;

    public CompraRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Compra>> GetAllAsync()
    {
        return await _context.Compras
            .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
            .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
            .Include(p => p.MedicamentosComprados)
            .ToListAsync();
    }
    
    /*.Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
            .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
            .Include(p => p.MedicamentosComprados)*/

    public async Task<IEnumerable<object>> GetAllWiMe()
{
    var compras = await (
        from c in _context.Compras
        join mc in _context.MedicamentosComprados on c.Id equals mc.CompraIdFk
        join me in _context.Medicamentos on mc.MedicamentoIdFk equals me.Id
        select new 
        {
            idCompra = c.Id,
            fechaCompra = c.FechaCompra,
            proveedor = c.ProveedorIdFk,
            medicamentosComprados = new 
            {
                id = mc.Id,
                Nombre = me.Nombre,
                compraIdFk = mc.CompraIdFk,
                MedicamentoIdFk = mc.MedicamentoIdFk,
                CantidadComprada = mc.CantidadComprada,
                PrecioCompra = mc.PrecioCompra
                // Agrega aquí más propiedades de MedicamentosComprados si las necesitas
            }
        }
    ).ToListAsync();

    // Agrupar los resultados por compra
    var comprasAgrupadas = compras.GroupBy(c => new { c.idCompra, c.fechaCompra, c.proveedor })
        .Select(g => new 
        {
            idCompra = g.Key.idCompra,
            fechaCompra = g.Key.fechaCompra,
            proveedor = g.Key.proveedor,
            medicamentosComprados = g.Select(c => c.medicamentosComprados)
        });

    return comprasAgrupadas;
}


    public override async Task<Compra> GetByIdAsync(int id)
    {
        return await _context.Compras
        .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
        .Include(p => p.MedicamentosComprados)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

}
