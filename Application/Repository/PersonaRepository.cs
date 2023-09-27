

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    protected readonly FarmaciaContext _context;

    public PersonaRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas
        .Include(p => p.TipoPersona).ThenInclude(p => p.Nombre)
        .Include(p => p.TipoDocumento).ThenInclude(p => p.Nombre)
        .ToListAsync();
    }

    public override async Task<Persona> GetByIdAsync(int id)
    {
        return await _context.Personas
        .Include(p => p.TipoPersona)
        .Include(p => p.TipoDocumento)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
