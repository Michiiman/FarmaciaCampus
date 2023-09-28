using Domain.Entities;

namespace ApiFarmacia.Dtos;
public class MedicamentoRecetaDto
{
    public int Id { get; set;}
    public int MedicamentosIdFk { get; set; }
    public int RecetaIdFk { get; set; }
    public string Descripcion { get; set; }
    public int Cantidad { get; set; }
    public MedicamentoDto MedicamentoDto { get; set; }
}
