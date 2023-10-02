using Domain.Entities;

namespace Domain.Interfaces;

    public interface ICompra : IGenericRepo<Compra>
    {
        Task<IEnumerable<object>> GetAllWiMe();
    }
