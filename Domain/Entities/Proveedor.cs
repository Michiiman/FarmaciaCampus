
namespace Domain.Entities;

public class Proveedor : BaseEntity
{
    public string Nombre { get; set; }
    public string Contacto{ get; set; }
    public string Direccion{ get; set; }
    public ICollection<Proveedor> Proveedores{ get; set; }
    
}
