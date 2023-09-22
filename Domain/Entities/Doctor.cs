

namespace Domain.Entities;

    public class Doctor : BaseEntity
    {
        public string Nombre { get; set; }
        public string Especialidad{ get; set; }
        public ICollection<Receta>Recetas{ get; set; }
    }
