

namespace Domain.Entities;

public class Receta : BaseEntity
{
    public DateTime FechaExpedicion { get; set; }
    public int PacienteIdFk { get; set; }//Paciente_Id
    public Persona Paciente { get; set; }
    public int DoctorIdFk { get; set; } //doctor_Id
    public Persona Doctor { get; set; }
    public string Descripcion { get; set; }
    public FacturaVenta FacturaVenta { get; set; }
    public ICollection<MedicamentoReceta> MedicamentosRecetas{ get; set; }

}
