

using Domain.Entities;

namespace ApiFarmacia.Dtos;

public class FacturaVentaDto
{
    public int Id { get; set; }
    public DateTime FechaFactura{ get; set; }
    public int  PacienteIdFk{ get; set; }
    public PersonaDto Paciente{ get; set; }
    public int EmpleadoIdFk{ get; set; }
    public PersonaDto Empleado{ get; set; }
    public int RecetaIdFk{ get; set; }
    public RecetaDto Receta{ get; set; }
    public string PrecioTotal{ get; set; }
    
}
