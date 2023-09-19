
namespace Domain.Entities;

public class MedicamentoVendido : BaseEntity
{
    public int VentaIdFk{ get; set; }
    public Venta Venta{ get; set; }
    public int MedicamentoIdFk{ get; set; }
    public Medicamento Medicamento{ get; set; }
    public int CantidadVendida{ get; set; }
    public int Precio{ get; set; }

}
