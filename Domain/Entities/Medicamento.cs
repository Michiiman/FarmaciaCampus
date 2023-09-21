
namespace Domain.Entities;

public class Medicamento : BaseEntity
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public string TipoMedicamento { get; set; }
    public int ProveedorIdFk { get; set; }
    public Persona ProveedorId { get; set; }
    public ICollection<MedicamentoVendido> MedicamentoVendidos { get; set; }
    public ICollection<MedicamentoComprado> MedicamentosComprados { get; set; }
    public ICollection<MedicamentoReceta> MedicamentosRecetas { get; set; }

}
