using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicamentoVendidoRepository : GenericRepository<MedicamentoVendido>,IMedicamentoVendido
{
    protected readonly FarmaciaContext _context;

    public MedicamentoVendidoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicamentoVendido>> GetAllAsync()
    {
        return await _context.MedicamentoVendidos
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Paciente).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Empleado).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Paciente).ThenInclude(p=>p.TipoDocumento)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Empleado).ThenInclude(p=>p.TipoDocumento)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Receta)
        .Include(p => p.Medicamento).ThenInclude(p=>p.Persona).ThenInclude(p=>p.TipoPersona)
        .ToListAsync();
    }

    public override async Task<MedicamentoVendido> GetByIdAsync(int id)
    {
        return await _context.MedicamentoVendidos
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Paciente).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Empleado).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Receta)
        .Include(p => p.Medicamento).ThenInclude(p=>p.Persona)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    
 

}
