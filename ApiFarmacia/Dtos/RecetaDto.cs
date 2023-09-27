

namespace ApiFarmacia.Dtos;

public class RecetaDto
{
    public int Id { get; set;}
    public DateTime FechaExpedicion{ get; set;}
    public int PacienteIdFk { get; set; }
    public PersonaDto Persona{ get; set; }
    
   /*  public int DoctorIdFk { get; set; }
    public PersonaDto Doctor{ get; set; } */
}
