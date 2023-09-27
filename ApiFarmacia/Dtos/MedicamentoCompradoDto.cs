using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFarmacia.Dtos;
public class MedicamentoCompradoDto
{
    public int Id { get; set; }
    public int CompraIdFk { get; set; }
    public int MedicamentoIdFk { get; set; }
    public int CantidadComprada { get; set; }
    public int PrecioCompra { get; set; }
    public MedicamentoDto Medicamento { get; set; }
}