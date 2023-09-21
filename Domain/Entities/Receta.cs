

namespace Domain.Entities;

public class Receta : BaseEntity
{
    public DateTime FechaExpedicion { get; set; }
    public int PacienteIdFk { get; set; }//Paciente_Id
    public Persona PacienteId { get; set; }
    public int DoctorIdFk { get; set; } //doctor_Id
    public Persona DoctorId { get; set; }
    public int MedicamentosRecetaIdFk { get; set; }
    public MedicamentoReceta MedicamentoRecetaId { get; set; }
    public string Descripcion { get; set; }

}
