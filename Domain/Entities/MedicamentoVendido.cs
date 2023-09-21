
namespace Domain.Entities;

public class MedicamentoVendido : BaseEntity
{
    public int FacturaVentaIdFk { get; set; }
    public FacturaVenta FacturaVentaId { get; set; }
    public int MedicamentoIdFk { get; set; }
    public Medicamento Medicamento { get; set; }
    public int CantidadVendida { get; set; }
    public int Precio { get; set; }

}
