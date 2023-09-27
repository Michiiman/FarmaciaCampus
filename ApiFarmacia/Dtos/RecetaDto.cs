namespace ApiFarmacia.Dtos;
public class RecetaDto
{
    public int Id { get; set;}
    public DateTime FechaExpidicion { get; set; }
    public int PacienteIdFk { get; set; }
    public int DoctorIdFk { get; set; }
    public string Descripcion { get; set; }
    public PersonaDto Persona { get; set; }
}
