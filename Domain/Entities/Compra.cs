
namespace Domain.Entities;

public class Compra : BaseEntity
{
    public DateTime FechaCompra { get; set; }
    public int ProveedorIdFk { get; set; }
    public Proveedor Proveedor { get; set; }

}
