namespace ApiFarmacia.Dtos;
public class CompraDto
{
    public int Id { get; set; }
    public DateTime FechaCompra { get; set; }
    public int ProvedorIdFk { get; set; }
    public PersonaDto Persona { get; set; }
    public MedicamentoCompradoDto MedicamentoComprado{ get; set; }
}
