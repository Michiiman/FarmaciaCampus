using Domain.Entities;

namespace Domain.Interfaces;

public interface IPersona : IGenericRepo<Persona>
{
    Task<IEnumerable<Persona>> GetPacientesCompraronParacetamol(string data );
    Task<IEnumerable<Persona>> GetPacientesCompraronParacetamolEn2023(string data,int año);
    Task<IEnumerable<Persona>> GetPersonaQueNoHaComprado2023(int año);
    Task<Object> GetPersonaQueMasComproEnAño(int año);
    Task<IEnumerable<object>> GetTotalGastadoPorPacienteEnAño(int año);

}
