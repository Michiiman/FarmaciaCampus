
namespace Domain.Entities;

public class MedicamentoReceta : BaseEntity
{
    public int MedicamentoIdFk { get; set; }
    public Medicamento Medicamento { get; set; }
    public int RecetaIdFk { get; set; }
    public Receta Receta { get; set; }
    public string Descripcion { get; set; }
<<<<<<< HEAD
    public int Cantidad{    get; set; }
=======
    public int Cantidad { get; set; }
>>>>>>> dcd7fc75281367ae22583f90ab7b707a3ce691ea

}
