using Domain.Entities;

namespace Domain.Interfaces;

public interface IPersona : IGenericRepo<Persona>
{
    Task<IEnumerable<Persona>> GetPacientesCompraronParacetamol(string data );

}
