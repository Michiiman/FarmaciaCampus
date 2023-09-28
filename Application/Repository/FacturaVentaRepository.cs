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
        .Include(p => p.Paciente).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Paciente).ThenInclude(p => p.TipoDocumento)
        .Include(p => p.Empleado).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.Empleado).ThenInclude(p=>p.TipoDocumento)
        .Include(p => p.Receta).ThenInclude(p => p.Doctor)
        .ToListAsync();
    }

    public override async Task<FacturaVenta> GetByIdAsync(int id)
    {
        return await _context.FacturasVentas
        .Include(p => p.Paciente).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Paciente).ThenInclude(p => p.TipoDocumento)
        .Include(p => p.Empleado).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.Empleado).ThenInclude(p=>p.TipoDocumento)
        .Include(p => p.Receta).ThenInclude(p => p.Doctor)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
