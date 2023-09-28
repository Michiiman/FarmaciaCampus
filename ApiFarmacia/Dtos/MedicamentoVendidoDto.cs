

namespace ApiFarmacia.Dtos;

    public class MedicamentoVendidoDto
    {
        public int Id{ get; set; }
        public int FacturaVentaIdFk{ get; set; }
        public FacturaVentaDto FacturaVenta{ get; set; }
        public int MedicamentoIdFk{ get; set; }
        public MedicamentoDto Medicamento{ get; set; }
    }
