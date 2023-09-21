
namespace Domain.Entities;

public class FacturaVenta : BaseEntity
{
    public DateTime FechaFactura { get; set; }
    public int PacienteIdFk { get; set; }
    public Persona PacienteId { get; set; }
    public int EmpleadoIdFk { get; set; }
    public Persona EmpleadoId { get; set; }
    public int RecetaId { get; set; }
    public Receta Receta { get; set; }
    public int PrecioTotal { get; set; }
    public ICollection<MedicamentoVendido> MedicamentoVendidos { get; set; }

}
