
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
        //.OrderByDescending(p. => p.TipoDocumento)
        .ToListAsync();
    }

    public override async Task<Medicamento> GetByIdAsync(int id)
    {
        return await _context.Medicamentos
        .Include(p => p.Persona).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Persona).ThenInclude(p => p.TipoDocumento)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    //controllers para metodos especificos
    public async Task<IEnumerable<Medicamento>> GetLess50()
    {
        return await _context.Medicamentos
            .Where(me => me.Stock < 50)
            .ToListAsync();
    }

    public async Task<IEnumerable<object>> GetProveedorName(string proveedor)
    {
        var medicamentos = await
        (
            from me in _context.Medicamentos
            join p in _context.Personas on me.ProveedorIdFk equals p.Id
            where (p.Nombre.Contains(proveedor) && p.Id == 5)
            select new
            {
                IdProvedor = me.ProveedorIdFk,
                Id = me.Id,
                Nombre = me.Nombre,
                PrecioVenta = me.Precio,
                Stock = me.Stock,
                FechaExpiracion = me.FechaExpiracion,
                TipoMedicamento = me.TipoMedicamento
            }
           // OrderByDescending(me => me.Id)
            
        ).ToListAsync();
        return medicamentos;
    }
}