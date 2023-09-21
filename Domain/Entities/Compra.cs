
namespace Domain.Entities;

public class Compra : BaseEntity
{
    public DateTime FechaCompra { get; set; }
    public int PersonaIdFk { get; set; } //Proveedor
    public Persona Persona { get; set; }

}
