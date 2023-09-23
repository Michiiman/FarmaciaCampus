using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class FacturaVentaRepository : GenericRepository<FacturaVenta>, IFacturaVenta
{        
    protected readonly FarmaciaContext _context;

    public  FacturaVentaRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<FacturaVenta>> GetAllAsync()
    {
        return await _context.FacturasVentas
        .Include(p => p.Paciente)
        .Include(p => p.Empleado)
        .Include(p => p.Receta)
        .ToListAsync();
    }

    public override async Task<FacturaVenta> GetByIdAsync(int id)
    {
        return await _context.FacturasVentas
        .Include(p => p.Paciente)
        .Include(p => p.Empleado)
        .Include(p => p.Receta)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
