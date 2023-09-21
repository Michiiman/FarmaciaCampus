
namespace Domain.Entities;

public class Medicamento : BaseEntity
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public int Stock{ get; set; }
    public DateTime FechaExpiracion{ get; set; }
    public int PersonaIdFk{ get; set; } //Proveedor
    public Persona Persona{ get; set; }

}
/*{"medicamentos": 
        {
        "id":"001"    
        "nombres" :"Paracetamol",
        "precio":"15000",
        "stock":"50",
        "fechaexpiracion":"01/02/2024",
        "proveedor":
        {
            "nombre"   :"bayern",
            "telefono": "315465498",
            "direccion":"fdsgldskfg13",
            "TipoPersona": "proveedor"
        }
    },
     {
        "id":"002"
        "nombres" :"Paracetamol",
        "precio":"12000",
        "stock":"50",
        "fechaexpiracion":"01/12/2023",
        "proveedor":
        {
            "nombre"   :"Mk",
            "telefono": "315465498",
            "direccion":"fdsgldskfg13",
            "TipoPersona": "proveedor"
        }
}*/