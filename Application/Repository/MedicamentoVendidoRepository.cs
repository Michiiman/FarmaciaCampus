
using Domain.Entities;
using Domain.Interfaces;
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
        .Include(p => p.FacturaVenta)
        .Include(p => p.Medicamento)
        .ToListAsync();
    }

    public override async Task<MedicamentoVendido> GetByIdAsync(int id)
    {
        return await _context.MedicamentoVendidos
        .Include(p => p.FacturaVenta)
        .Include(p => p.Medicamento) 
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
