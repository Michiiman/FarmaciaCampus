

namespace Domain.Entities;

    public class Telefono : BaseEntity
    {
        public int PersonaIdFk { get; set; }
        public Persona Persona { get; set; }
        public string TipoTelefono{ get; set; }
        public int Numero{ get; set; }
    }
