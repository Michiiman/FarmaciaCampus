<<<<<<< HEAD


using Domain.Entities;

namespace ApiFarmacia.Dtos
{
    public class CompraDto
    {
        public int Id{ get; set; }
        public DateTime FechaCompra{ get; set; }
        public int ProveedorIdFk { get; set; }
        //public MedicamentoCompradoDto MedicamentoComprado{ get; set; }

    }
}
=======
namespace ApiFarmacia.Dtos;
public class CompraDto
{
    public int Id { get; set; }
    public DateTime FechaCompra { get; set; }
    public int ProveedorIdFk { get; set; }
    public PersonaDto Persona { get; set; }
    public List<MedicamentoCompradoDto> MedicamentoComprados{ get; set; }
}
>>>>>>> Alejandro
