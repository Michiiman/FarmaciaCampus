
namespace Domain.Entities;

public class MedicamentoReceta : BaseEntity
{
    public int MedicamentosIdfk { get; set; }
    public Medicamento Medicamento { get; set; }
    public int RecetaId { get; set; }
    public Receta Receta { get; set; }

}
