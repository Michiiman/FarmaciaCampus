

namespace Domain.Entities;

public class TipoPersona : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<Persona> Personas{ get; set; }

}
