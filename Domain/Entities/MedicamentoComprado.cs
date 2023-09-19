

namespace Domain.Entities;

public class MedicamentoComprado : BaseEntity
{
    public int CompraIdFk { get; set; }
    public Compra Compra { get; set; }
    public int MedicamentoIdFk { get; set; }
    public Medicamento Medicamento { get; set; }
    public int CantidadComprada { get; set; }
    public int PrecioCompra { get; set; }
    public ICollection<Compra> Compras{ get; set; }


}
