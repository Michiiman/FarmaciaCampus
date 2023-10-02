
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
        var medicamentos = await
        (
            from me in _context.Medicamentos
            where(me.Stock < 50)
            orderby (me.Id)
            select me
        ).ToListAsync();
        return medicamentos;
    }

    public async Task<IEnumerable<object>> GetProveedorName(string proveedor)
    {
        var medicamentos = await
        (
            from me in _context.Medicamentos
            join p in _context.Personas on me.ProveedorIdFk equals p.Id
            join tp in _context.TiposPersonas on p.TipoPersonaIdFk equals tp.Id
            where (p.Nombre.Contains(proveedor) && tp.Id == 5)
            orderby(me.Id)
            select new
            {
                IdProvedor = me.ProveedorIdFk,
                NombreProveedor = p.Nombre,
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

    public async Task<IEnumerable<Medicamento>> GetAfterDate(DateTime date)
    {
        
            var medicamentos = await (
                from me in _context.Medicamentos
                where (me.FechaExpiracion < date)
                orderby(me.FechaExpiracion)
                select me
            ).ToListAsync();
            return medicamentos;
    }
    public async Task<Medicamento> GetMostExpensive()
    {
        var medicamento = await (
                from me in _context.Medicamentos
                orderby(me.Precio) descending
                select me
            ).FirstOrDefaultAsync();
            return medicamento;
    }

    public async Task<IEnumerable<Medicamento>> GetHighherPriceAndUnderStock(int price, int stock)
    {
        var medicamento = await (
                from me in _context.Medicamentos
                where (me.Precio > price && me.Stock < stock)
                orderby(me.Id) descending
                select me
            ).ToListAsync();
            return medicamento;
    }
    public async Task<IEnumerable<Medicamento>> GetExpireYear(int year)
    {
        
            var medicamentos = await (
                from me in _context.Medicamentos
                where (me.FechaExpiracion.Year == year)
                orderby(me.FechaExpiracion)
                select me
            ).ToListAsync();
            return medicamentos;
    }
}