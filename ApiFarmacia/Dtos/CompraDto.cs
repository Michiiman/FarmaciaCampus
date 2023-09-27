

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