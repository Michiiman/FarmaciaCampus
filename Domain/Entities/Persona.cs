

namespace Domain.Entities;

public class Persona : BaseEntity
{
    public string Nombre { get; set; }
    public int TipoDeDocumentoIdFk{ get; set; }
    public int TelefonoIdFk{ get; set; }
    public string Direccion { get; set; }
    public int TipoPersonaId { get; set; }
    public TipoPersona TipoPersona { get; set; }

}
