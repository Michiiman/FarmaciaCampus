
using System.Threading.Tasks.Dataflow;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
{
    protected readonly FarmaciaContext _context;

    public MedicamentoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Medicamento>> GetAllAsync()
    {
        return await _context.Medicamentos
        .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
        .ToListAsync();
    }

    public override async Task<Medicamento> GetByIdAsync(int id)
    {
        return await _context.Medicamentos
        .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Medicamento>> GetMedicamentoMenosDe50(int cantidad)
    {

        return await _context.Medicamentos
            .Where(p => p.Stock < cantidad)
            .OrderByDescending(p => p.Id)
            .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
            .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
            .Take(cantidad)
            .ToListAsync();

    }
}