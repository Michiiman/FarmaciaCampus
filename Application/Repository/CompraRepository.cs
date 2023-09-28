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
    
    /*
    public override async Task<IEnumerable<Compra>> GetAllWiMe()
    {
        var compras = await (
            from c in _context.Compras
            join m in _context.MedicamentosComprados on c.Id equals m.CompraIdFk
            .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
            .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
            .Include(p => p.MedicamentosComprados)
            .ToListAsync()
            );
        
        return compras;
    }
    */

    public override async Task<Compra> GetByIdAsync(int id)
    {
        return await _context.Compras
        .Include(p => p.Persona)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

}
