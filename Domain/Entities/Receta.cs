

namespace Domain.Entities;

public class Receta : BaseEntity
{
    public DateTime FechaExpedicion { get; set; }
    public int PersonaIdFk { get; set; }//Paciente_Id
    public int PersonaId { get; set; } //doctor_Id
    public int MedicamentosRecetaId { get; set; }
    public string Descripcion { get; set; }

}
