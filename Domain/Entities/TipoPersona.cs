

namespace Domain.Entities;

public class TipoPersona : BaseEntity
{
    public string TipoDePersona { get; set; }
    public ICollection<Persona> Personas{ get; set; }

}
