using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento : IGenericRepo<Medicamento>
    {
        Task<IEnumerable<Medicamento>> GetLess50();
        Task<IEnumerable<object>> GetProveedorName(string proveedor);
        Task<IEnumerable<Medicamento>> GetAfterDate(DateTime date);
        Task<Medicamento> GetMostExpensive();
        Task<IEnumerable<Medicamento>> GetHighherPriceAndUnderStock(int price, int stock);
        Task<IEnumerable<Medicamento>> GetExpireYear(int year);
    }
