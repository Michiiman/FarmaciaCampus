using Domain.Entities;

namespace ApiFarmacia.Dtos;
public class MedicamentoRecetaDto
{
    public int Id { get; set;}
    public RecetaDto Receta { get; set; }
    public int DoctorIdFk { get; set; }
}
