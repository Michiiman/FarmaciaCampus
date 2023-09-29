

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
        .Include(p => p.TipoPersona)
        .Include(p => p.TipoDocumento)
        .ToListAsync();
    }

    public override async Task<Persona> GetByIdAsync(int id)
    {
        return await _context.Personas
        .Include(p => p.TipoPersona)
        .Include(p => p.TipoDocumento)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Persona>> GetPacientesCompraronParacetamol(string data )
    {

        var pacientes = await (from mv in _context.MedicamentoVendidos
                        join m in _context.Medicamentos on mv.MedicamentoIdFk equals m.Id
                        join fv in _context.FacturasVentas on mv.FacturaVentaIdFk equals  fv.Id
                        join p in _context.Personas on fv.PacienteIdFk equals p.Id
                        where p.TipoPersonaIdFk == 3
                        where mv.Medicamento.Nombre == data
                        select new Persona
                        {
                            Id=p.Id,
                            Nombre= p.Nombre,
                            TipoDocumento=p.TipoDocumento,
                            NumeroDocumento=p.NumeroDocumento,
                            Direccion=p.Direccion,
                            TipoPersona=p.TipoPersona

                        }).ToListAsync();

        
            return pacientes;
    }
}
