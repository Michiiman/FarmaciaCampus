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
    Task<IEnumerable<Persona>> GetPacientesCompraronParacetamol(string data );
    Task<IEnumerable<Persona>> GetPacientesCompraronParacetamolEn2023(string data,int año);
    Task<IEnumerable<Persona>> GetPersonaQueNoHaComprado2023(int año);
    Task<Object> GetPersonaQueMasComproEnAño(int año);
    Task<IEnumerable<object>> GetTotalGastadoPorPacienteEnAño(int año);
}
