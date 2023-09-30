using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento : IGenericRepo<Medicamento>
    {
        Task<IEnumerable<Medicamento>> GetLess50();
        Task<IEnumerable<object>> GetProveedorName(string proveedor);
    }
