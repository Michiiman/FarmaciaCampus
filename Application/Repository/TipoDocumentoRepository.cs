
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class TipoDocumentoRepository : GenericRepository<TipoDocumento>, ITipoDocumento
{
    protected readonly FarmaciaContext _context;

    public TipoDocumentoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoDocumento>> GetAllAsync()
    {
        return await _context.TiposDocumentos
        .ToListAsync();
    }

    public override async Task<TipoDocumento> GetByIdAsync(int id)
    {
        return await _context.TiposDocumentos
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
