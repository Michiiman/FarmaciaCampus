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
        .Include(p => p.Persona)
        .ToListAsync();
    }

    public override async Task<Compra> GetByIdAsync(int id)
    {
        return await _context.Compras
        .Include(p => p.Persona)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
