
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class TelefonoRepository : GenericRepository<Telefono>, ITelefono
{
    protected readonly FarmaciaContext _context;

    public TelefonoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Telefono>> GetAllAsync()
    {
        return await _context.Telefonos
        .Include(p => p.Persona)
        .ToListAsync();
    }

    public override async Task<Telefono> GetByIdAsync(int id)
    {
        return await _context.Telefonos
        .Include(p => p.Persona)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
