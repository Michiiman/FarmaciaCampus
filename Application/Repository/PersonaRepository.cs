

using System.Security.Cryptography.X509Certificates;
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

    public async Task<IEnumerable<Persona>> GetPacientesCompraronParacetamol(string data)
    {

        var pacientes = await (from mv in _context.MedicamentoVendidos
                                join m in _context.Medicamentos on mv.MedicamentoIdFk equals m.Id
                                join fv in _context.FacturasVentas on mv.FacturaVentaIdFk equals fv.Id
                                join p in _context.Personas on fv.PacienteIdFk equals p.Id
                                where p.TipoPersonaIdFk == 3
                                where mv.Medicamento.Nombre == data
                                select new Persona
                                {
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    TipoDocumento = p.TipoDocumento,
                                    NumeroDocumento = p.NumeroDocumento,
                                    Direccion = p.Direccion,
                                    TipoPersona = p.TipoPersona

                                }).ToListAsync();


        return pacientes;
    }

    public async Task<IEnumerable<Persona>> GetPacientesCompraronParacetamolEn2023(string data, int año)
    {
        DateTime fechaInicio = new DateTime(año, 01, 01);
        DateTime fechaFin = new DateTime(año, 12, 31);

        var pacientes = await (from mv in _context.MedicamentoVendidos
                                join m in _context.Medicamentos on mv.MedicamentoIdFk equals m.Id
                                join fv in _context.FacturasVentas on mv.FacturaVentaIdFk equals fv.Id
                                join p in _context.Personas on fv.PacienteIdFk equals p.Id
                                where mv.Medicamento.Nombre == data
                                where p.TipoPersonaIdFk == 3
                                where fv.FechaFactura >= fechaInicio && fv.FechaFactura <= fechaFin
                                select new Persona
                                {
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    TipoDocumento = p.TipoDocumento,
                                    NumeroDocumento = p.NumeroDocumento,
                                    Direccion = p.Direccion,
                                    TipoPersona = p.TipoPersona

                                }).ToListAsync();

        return pacientes;
    }

    public async Task<Object> GetPersonaQueMasComproEnAño(int año)
{
    DateTime fechaInicio = new DateTime(año, 1, 1);
    DateTime fechaFin = new DateTime(año, 12, 31);

        var personasConCompras = await (from fv in _context.FacturasVentas
                                    where fv.FechaFactura >= fechaInicio && fv.FechaFactura <= fechaFin
                                    select new
                                    {
                                        PacienteId = fv.PacienteIdFk,
                                        PrecioTotal = fv.PrecioTotal
                                    }).ToListAsync();

        var personasConTotalCompras = from pc in personasConCompras
                                    group pc by pc.PacienteId into g
                                    select new
                                    {
                                        PacienteId = g.Key,
                                        TotalCompras = g.Sum(x => x.PrecioTotal)
                                    };

        var personaQueMasCompro = (from ptc in personasConTotalCompras 
                                join p in _context.Personas on ptc.PacienteId equals p.Id
                                orderby ptc.TotalCompras descending
                                select new
                                {
                                    Id=p.Id,
                                    Nombre = p.Nombre,
                                    TotalCompras = ptc.TotalCompras
                                }).FirstOrDefault();

    return personaQueMasCompro;
}

    public async Task<IEnumerable<Persona>> GetPersonaQueNoHaComprado2023(int año)
    {
        DateTime fechaInicio = new DateTime(año, 1, 1);
        DateTime fechaFin = new DateTime(año, 12, 31);

        var personasConCompras = await (from fv in _context.FacturasVentas
                                        where fv.FechaFactura >= fechaInicio && fv.FechaFactura <= fechaFin
                                        select fv.PacienteIdFk).ToListAsync();

        var personasSinCompras = await (from p in _context.Personas
                                        where p.TipoPersonaIdFk == 3 && !personasConCompras.Contains(p.Id)
                                        select new Persona
                                        {
                                            Id = p.Id,
                                            Nombre = p.Nombre,
                                            TipoDocumento = p.TipoDocumento,
                                            NumeroDocumento = p.NumeroDocumento,
                                            Direccion = p.Direccion,
                                            TipoPersona = p.TipoPersona
                                        }).ToListAsync();

        return personasSinCompras;
    }


public async Task<IEnumerable<object>> GetTotalGastadoPorPacienteEnAño(int año)
{
    DateTime fechaInicio = new DateTime(año, 1, 1);
    DateTime fechaFin = new DateTime(año, 12, 31);

    var totalGastadoEnAño = await (from fv in _context.FacturasVentas
                                where fv.FechaFactura >= fechaInicio && fv.FechaFactura <= fechaFin
                                group fv by fv.PacienteIdFk into g
                                select new
                                {
                                    PacienteIdFk = g.Key,
                                    TotalGastado = g.Sum(x => x.PrecioTotal)
                                }).ToListAsync();

    var personas = await _context.Personas
        .Where(p => p.TipoPersonaIdFk == 3) 
        .Select(p => new
        {
            PacienteIdFk = p.Id,
            Nombre = p.Nombre
        })
        .ToListAsync();

    var resultado = from p in personas
                    join tg in totalGastadoEnAño on p.PacienteIdFk equals tg.PacienteIdFk into temp
                    from tg in temp.DefaultIfEmpty()
                    select new
                    {
                        PacienteIdFk = p.PacienteIdFk,
                        Nombre = p.Nombre,
                        TotalGastado = tg != null ? tg.TotalGastado : 0
                    };

    return resultado;
}



}
