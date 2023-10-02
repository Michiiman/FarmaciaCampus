
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class RecetaRepository : GenericRepository<Receta>, IReceta
{
    protected readonly FarmaciaContext _context;

    public RecetaRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Receta>> GetAllAsync()
    {
        return await _context.Recetas
        .Include(p => p.Paciente).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Paciente).ThenInclude(p => p.TipoDocumento)
        .Include(p => p.Doctor).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Doctor).ThenInclude(p => p.TipoDocumento)
        .ToListAsync();
    }

    public override async Task<Receta> GetByIdAsync(int id)
    {
        return await _context.Recetas
        .Include(p => p.Paciente).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Paciente).ThenInclude(p => p.TipoDocumento)
        .Include(p => p.Doctor).ThenInclude(p => p.TipoPersona)
        .Include(p => p.Doctor).ThenInclude(p => p.TipoDocumento)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    //metodos personalizados para enpoints

    //4. Obtener recetas médicas emitidas después del 1 de enero de 2023.
    public async Task<IEnumerable<object>> GetBeforeJune(int year)
    {
        var fecha = new DateTime(year, 1, 1);
        var recetas = await (
            from r in _context.Recetas
            where (r.FechaExpedicion > fecha)
            join p in _context.Personas on r.PacienteIdFk equals p.Id
            join d in _context.Personas on r.DoctorIdFk equals d.Id
            join mr in _context.MedicamentosRecetas on r.Id equals mr.RecetaIdFk
            join m in _context.Medicamentos on mr.MedicamentoIdFk equals m.Id
            select new
            {
                IdReceta = r.Id,
                FechaExpedicion = r.FechaExpedicion,
                IdPaciente = r.PacienteIdFk,
                NombrePaciente = p.Nombre,
                IdDoctor = r.DoctorIdFk,
                NombreDoctor = d.Nombre,
                NombreMedicamentos = m.Nombre,
                Cantidad = mr.Cantidad,
                Descripcion = mr.Descripcion
            }
        )
        .GroupBy(r => r.IdReceta)
        .Select(grp => new
        {
            IdReceta = grp.Key,
            grp.First().FechaExpedicion,
            grp.First().IdPaciente,
            grp.First().NombrePaciente,
            grp.First().IdDoctor,
            grp.First().NombreDoctor,
            Medicamentos = grp.Select(m => new
            {
                Nombre = m.NombreMedicamentos,
                Cantidad = m.Cantidad,
                Descripcion = m.Descripcion
            })
        })
        .ToListAsync();

        return recetas;
    }
}
