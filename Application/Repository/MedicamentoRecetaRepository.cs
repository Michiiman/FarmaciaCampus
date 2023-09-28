
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicamentoRecetaRepository : GenericRepository<MedicamentoReceta>, IMedicamentoReceta
{
    protected readonly FarmaciaContext _context;

    public MedicamentoRecetaRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicamentoReceta>> GetAllAsync()
    {
        return await _context.MedicamentosRecetas
        .Include(p => p.Medicamento)
        .Include(p => p.Receta)
        .ToListAsync();
    }

    public override async Task<MedicamentoReceta> GetByIdAsync(int id)
    {
        return await _context.MedicamentosRecetas
        .Include(p => p.Medicamento).ThenInclude(p => p.Persona)
        .Include(p => p.Receta)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
