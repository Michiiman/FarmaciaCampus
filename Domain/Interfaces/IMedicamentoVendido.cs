

using Domain.Entities;

namespace Domain.Interfaces;

public interface IMedicamentoVendido : IGenericRepo<MedicamentoVendido>
{
    Task<object> GetVentasMedicamento(string medicamento);
    Task<IEnumerable<object>> GetNoSales ();
}
