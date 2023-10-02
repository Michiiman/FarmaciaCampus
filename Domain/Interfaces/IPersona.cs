using Domain.Entities;

namespace Domain.Interfaces;

public interface IPersona : IGenericRepo<Persona>
{
    Task<IEnumerable<object>> GetSalesPerEmployeed(int year);
    Task<IEnumerable<object>> GetMoreThan5sales();
    Task<IEnumerable<object>> GetAnysales(int year);
    Task<IEnumerable<object>> Get0To5sales(int year);
    Task<object> GetBestSeller(int year);
    Task<IEnumerable<object>> GetAnySalePerMonthNYear(int year, int month);
}
