namespace ApiFarmacia.Dtos;
public class MedicamentoDto
{
    public int Id { get; set;}
    public string Nombre {get;set;}
    public int Precio {get; set;}
    public int Stock { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public string TipoMedicamento { get; set; }
    public int ProveedorId { get; set; }
    public MedicamentoVendidoDto MedicamentosVendidos { get; set; }
    public MedicamentoCompradoDto MedicamentosComprados { get; set; }
    public MedicamentoRecetaDto MedicamentosRecetas { get; set; }
}
