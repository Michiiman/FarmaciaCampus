

namespace Domain.Entities;

public class Persona : BaseEntity
{
    public string Nombre { get; set; }
    public int TipoDeDocumentoIdFk { get; set; }
    public TipoDocumento TipoDeDocumento { get; set; }
    public string NumeroDocumento {get;set;}
    public string Direccion { get; set; }
    public int TipoPersonaIdFk { get; set; }
    public TipoPersona TipoPersona { get; set; }
    public User User { get; set; }
    public ICollection<Telefono> Telefonos { get; set; }
    public ICollection<Compra> Compras { get; set; }
    public ICollection<Medicamento> Medicamentos { get; set; }
    public ICollection<Receta> Pacientes { get; set; }
    public ICollection<Receta> Doctores { get; set; }
    public ICollection<FacturaVenta> PacientesVentas { get; set; }
    public ICollection<FacturaVenta> EmpleadosVentas { get; set; }

}
