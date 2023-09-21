
namespace Domain.Entities;

public class Venta : BaseEntity
{
    public DateTime FechaVenta { get; set; }
    public int PacienteIdFk{ get; set; }//paciente
    public Persona PersonaPaciente{ get; set; }
    public int EmpleadoIdFk{ get; set; }//vendedor
    public Persona PersonaEmpleado { get; set; }
    
}
