using Domain.Entities;

namespace Domain.Interfaces;

public interface IRol : IGenericRepo<Rol>
{
    Task<Rol> GetByIdAsync(string id);
}
