
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicamentoVendidoRepository : GenericRepository<MedicamentoVendido>,IMedicamentoVendido
{
    protected readonly FarmaciaContext _context;

    public MedicamentoVendidoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicamentoVendido>> GetAllAsync()
    {
        return await _context.MedicamentoVendidos
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Paciente).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Empleado).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Paciente).ThenInclude(p=>p.TipoDocumento)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Empleado).ThenInclude(p=>p.TipoDocumento)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Receta)
        .Include(p => p.Medicamento).ThenInclude(p=>p.Persona).ThenInclude(p=>p.TipoPersona)
        .ToListAsync();
    }

    public override async Task<MedicamentoVendido> GetByIdAsync(int id)
    {
        return await _context.MedicamentoVendidos
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Paciente).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Empleado).ThenInclude(p=>p.TipoPersona)
        .Include(p => p.FacturaVenta).ThenInclude(p=>p.Receta)
        .Include(p => p.Medicamento).ThenInclude(p=>p.Persona)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    //5. Total de ventas del medicamento ‘Paracetamol’.
    public async Task<object> GetVentasMedicamento(string medicamento)
    {
        var medicamentos = await (
            from mv in _context.MedicamentoVendidos
            join me in _context.Medicamentos on mv.MedicamentoIdFk equals me.Id
            where(me.Nombre.Contains(medicamento))
            group new {mv, me} by new { mv.MedicamentoIdFk, me.Nombre } into grupo
            orderby grupo.Count() descending
            select new
            {
                IdMedicamento = grupo.Key.MedicamentoIdFk,
                NombreMedicamento = grupo.Key.Nombre,
                Medicamentos = grupo.Count()
            }
        ).ToListAsync();
        return medicamentos;
    }
    
    //9. Medicamentos que no han sido vendidos.
    public async Task<IEnumerable<object>> GetNoSales ()
    {
        var medicamentos = await (
            from me in _context.Medicamentos
            join mv in _context.MedicamentoVendidos on me.Id equals mv.MedicamentoIdFk into sales
            from s in sales.DefaultIfEmpty()
            where s == null
            select new
            {
                IdMedicamento = me.Id,
                NombreMedicamento = me.Nombre,
                FechaExpedicion = me.FechaExpiracion,
                TipoMedicamento = me.TipoMedicamento
            }
        ).ToListAsync();
        return medicamentos;
    }
    //14. Obtener el total de medicamentos vendidos en marzo de 2023.
    public async Task<IEnumerable<object>> GetSalesPerMounth (int year, int month)
    {
        var medicamentos = await (
            from mv in _context.MedicamentoVendidos
            join fv in _context.FacturasVentas on mv.FacturaVentaIdFk equals fv.Id
            where (fv.FechaFactura.Year == year && fv.FechaFactura.Month == month)
            group mv by mv.MedicamentoIdFk into grupo
            select new
            {
                IdMedicamento = grupo.Key,
                Ventas = grupo.Count()
                
            }
        ).ToListAsync();
        return medicamentos;
    }
    //15. Obtener el medicamento menos vendido en 2023.
    //17. Promedio de medicamentos vendidos por venta.
    //26. Total de medicamentos vendidos por mes en 2023.
    //31. Medicamentos que han sido vendidos cada mes del año 2023.
    //34. Medicamentos que no han sido vendidos en 2023.
    //36. Total de medicamentos vendidos en el primer trimestre de 2023.
}
