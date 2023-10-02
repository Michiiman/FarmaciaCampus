

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http.Features;
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

    //Cantidad de ventas realizadas por cada empleado en 2023.
    public async Task<IEnumerable<object>> GetSalesPerEmployeed(int year)
    {
        var empleados = await (
            from pe in _context.Personas
            join fv in _context.FacturasVentas on pe.Id equals fv.EmpleadoIdFk
            orderby (pe.Id)
            where (pe.TipoPersonaIdFk == 2)
            where (fv.FechaFactura.Year == year)

            group pe by pe.Id into grupo
            select new
            {
                IdEmpleado = grupo.Key,
                Ventas = grupo.Count()
            }
        ).ToListAsync();

        var detalle = (
            from pe in _context.Personas.AsEnumerable()
            join g in empleados on pe.Id equals g.IdEmpleado
            select new
            {
                EmpleadoId = pe.Id,
                Nombre = pe.Nombre,
                NumeroDocumento = pe.NumeroDocumento,
                g.Ventas
            }
        ).ToList();

        return detalle;
    }

    //Empleados que hayan hecho más de 5 ventas en total.
    public async Task<IEnumerable<object>> GetMoreThan5sales()
    {
        var empleados = await (
            from pe in _context.Personas
            join fv in _context.FacturasVentas on pe.Id equals fv.EmpleadoIdFk
            orderby (pe.Id)
            where (pe.TipoPersonaIdFk == 2)
            group pe by pe.Id into grupo
            select new
            {
                IdEmpleado = grupo.Key,
                Ventas = grupo.Count()
            }
        ).ToListAsync();

        var detalle = (
            from per in _context.Personas.AsEnumerable()
            join em in empleados on per.Id equals em.IdEmpleado
            where (em.Ventas >= 5)
            select new
            {
                EmpleadoId = per.Id,
                Nombre = per.Nombre,
                NumeroDocumento = per.NumeroDocumento,
                em.Ventas
            }
        ).ToList();

        return detalle;
    }
    //23. Empleados que no han realizado ninguna venta en 2023.
    public async Task<IEnumerable<object>> GetAnysales(int year)
    {
        var detalle = await (
            from pe in _context.Personas
            where pe.TipoPersonaIdFk == 2
            join fv in _context.FacturasVentas on pe.Id equals fv.EmpleadoIdFk into ventas
            from v in ventas.Where(fv => fv.FechaFactura.Year == year).DefaultIfEmpty()
            where v == null
            select new
            {
                EmpleadoId = pe.Id,
                Nombre = pe.Nombre,
                NumeroDocumento = pe.NumeroDocumento,
            }
        ).ToListAsync();

        return detalle;
    }
    //27. Empleados con menos de 5 ventas en 2023.
    public async Task<IEnumerable<object>> Get0To5sales(int year)
    {
        var empleados = await (
            from pe in _context.Personas
            join fv in _context.FacturasVentas on pe.Id equals fv.EmpleadoIdFk
            orderby (pe.Id)
            where (pe.TipoPersonaIdFk == 2)
            where (fv.FechaFactura.Year == year)
            group pe by pe.Id into grupo
            select new
            {
                IdEmpleado = grupo.Key,
                Ventas = grupo.Count()
            }
        ).ToListAsync();

        var detalle = (
            from per in _context.Personas.AsEnumerable()
            join em in empleados on per.Id equals em.IdEmpleado
            where (em.Ventas < 5)
            select new
            {
                EmpleadoId = per.Id,
                Nombre = per.Nombre,
                NumeroDocumento = per.NumeroDocumento,
                em.Ventas
            }
        ).ToList();

        return detalle;
    }
    //32. Empleado que ha vendido la mayor cantidad de medicamentos distintos en 2023.
    public async Task<object> GetBestSeller(int year)
    {
        var empleado = await (
            from pe in _context.Personas
            join fv in _context.FacturasVentas on pe.Id equals fv.EmpleadoIdFk
            join mv in _context.MedicamentoVendidos on fv.Id equals mv.FacturaVentaIdFk
            where (pe.TipoPersonaIdFk == 2 && fv.FechaFactura.Year == year)
            group new { fv, mv } by new { fv.EmpleadoIdFk } into grupo
            orderby grupo.Count() descending
            select new
            {
                IdEmpleado = grupo.Key.EmpleadoIdFk,
                Medicamentos = grupo.Count()
            }
        ).FirstOrDefaultAsync();
        return empleado;
    }

    //37. Empleados que no realizaron ventas en abril de 2023.
    public async Task<IEnumerable<object>> GetAnySalePerMonthNYear(int year, int month)
    {
        var detalle = await (
            from pe in _context.Personas
            where pe.TipoPersonaIdFk == 2
            join fv in _context.FacturasVentas on pe.Id equals fv.EmpleadoIdFk into ventas
            from v in ventas.Where
                (fv =>
                    fv.FechaFactura.Year == year && fv.FechaFactura.Month == month
                ).DefaultIfEmpty()
            where v == null
            select new
            {
                EmpleadoId = pe.Id,
                Nombre = pe.Nombre,
                NumeroDocumento = pe.NumeroDocumento,
            }
        ).ToListAsync();
        return detalle;
    }


}
