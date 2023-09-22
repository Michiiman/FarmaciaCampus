

namespace Domain.Entities;

public class TipoDocumento : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<Persona> Personas { get; set; }
}
