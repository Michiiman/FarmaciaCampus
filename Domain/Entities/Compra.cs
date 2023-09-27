
namespace Domain.Entities;

public class Compra : BaseEntity
{
    public DateTime FechaCompra { get; set; }
    public int ProveedorIdFk { get; set; } //Proveedor
    public Persona Persona { get; set; }
    public ICollection<MedicamentoComprado> MedicamentosComprados { get; set; }

}
