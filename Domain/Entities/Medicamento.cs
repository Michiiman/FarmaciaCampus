
namespace Domain.Entities;

public class Medicamento : BaseEntity
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public int Stock{ get; set; }
    public DateTime FechaExpiracion{ get; set; }
    public int ProveedorIdFk{ get; set; }
    public Proveedor Proveedor{ get; set; }

}
