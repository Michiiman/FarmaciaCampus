
namespace Domain.Entities;

public class Venta : BaseEntity
{
    public DateTime FechaVenta { get; set; }
    public int PacienteIdFk{ get; set; }
    public Paciente Paciente{ get; set; }
    public int EmpleadoIdFk{ get; set; }
    public Empleado Empleado{ get; set; }
}
