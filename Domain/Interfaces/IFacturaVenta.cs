using Domain.Entities;

namespace Domain.Interfaces;

    public interface IFacturaVenta : IGenericRepo<FacturaVenta>
    {
        Task<int> GetRecaudo();
    }
