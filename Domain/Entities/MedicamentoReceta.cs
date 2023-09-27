
namespace Domain.Entities;

public class MedicamentoReceta : BaseEntity
{
    public int MedicamentosIdfk { get; set; }
    public Medicamento Medicamento { get; set; }
    public int RecetaIdFk { get; set; }
    public Receta Receta { get; set; }
    public string Descripcion { get; set; }
    public int Cantidad{    get; set; }

}
