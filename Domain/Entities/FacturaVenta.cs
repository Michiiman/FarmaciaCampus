
namespace Domain.Entities;

public class FacturaVenta : BaseEntity
{
    public DateTime FechaFactura { get; set; }
    public int PacienteIdFk { get; set; }
    public Persona Paciente{ get; set; }
    public int EmpleadoIdFk { get; set; }
    public Persona Empleado { get; set; }
    public int RecetaIdFk { get; set; }
    public Receta Receta { get; set; }
    public string PrecioTotal { get; set; }
    public ICollection<MedicamentoVendido> MedicamentosVendidos { get; set; }

}
