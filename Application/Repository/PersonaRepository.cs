

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
                                        Id = p.Id,
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

    public async Task<IEnumerable<object>> GetProveedoresConMenosDe50Stock()
    {   
        var proveedores =  await (from m in _context.Medicamentos
                                join p in _context.Personas on m.ProveedorIdFk equals p.Id
                                where p.TipoPersonaIdFk == 5
                                where m.Stock < 50
                                select new 
                                {
                                    NombreMedicamento=m.Nombre,
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    NumeroDocumento = p.NumeroDocumento,
                                    Direccion = p.Direccion
                                }).ToListAsync();

        return proveedores;
    }

    public async Task<IEnumerable<object>> GetProveedoresMedicamentos()
    {   
        var proveedores =  await (from m in _context.Medicamentos
                                join p in _context.Personas on m.ProveedorIdFk equals p.Id
                                join t in _context.Telefonos on p.Id equals t.Id
                                where p.TipoPersonaIdFk == 5
                                select new 
                                {
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    NumeroDocumento = p.NumeroDocumento,
                                    Direccion = p.Direccion,
                                    Numero=t.Numero
                                }).ToListAsync();

        var distinctProveedores = proveedores.Distinct();

        return distinctProveedores;
    }
    

    public async Task<IEnumerable<object>> GetTotalMedicamentosVendidosPorProveedor()
    {   
        var totalVendidos = await (from mv in _context.MedicamentoVendidos
                                join m in _context.Medicamentos on mv.MedicamentoIdFk equals m.Id
                                join p in _context.Personas on m.ProveedorIdFk equals p.Id
                                group mv by new { ProveedorId = p.Id, ProveedorNombre = p.Nombre } into g
                                select new 
                                {
                                    ProveedorId = g.Key.ProveedorId,
                                    ProveedorNombre = g.Key.ProveedorNombre,
                                    TotalVendido = g.Sum(x => x.CantidadVendida)
                                }).ToListAsync();

        return totalVendidos;
    }

    public async Task<object> GetProveedorQueHaDadoMasMedicamentos()
    {   
        DateTime fechaInicio = new DateTime(2023, 1, 1);
        DateTime fechaFin = new DateTime(2023, 12, 31);

        var proveedor = await (from m in _context.Medicamentos
                            join p in _context.Personas on m.ProveedorIdFk equals p.Id
                            join c in _context.Compras on p.Id equals c.ProveedorIdFk
                            where c.FechaCompra >= fechaInicio && c.FechaCompra <= fechaFin
                            group m by new { ProveedorId = p.Id, ProveedorNombre = p.Nombre } into g
                            orderby g.Sum(m => m.Stock) descending
                            select new 
                            {
                                ProveedorId = g.Key.ProveedorId,
                                ProveedorNombre = g.Key.ProveedorNombre,
                                TotalMedicamentos = g.Sum(m => m.Stock)
                            }).FirstOrDefaultAsync();

        return proveedor;
    }


}