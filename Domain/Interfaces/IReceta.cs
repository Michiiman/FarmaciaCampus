

using Domain.Entities;

namespace Domain.Interfaces;

    public interface IReceta : IGenericRepo<Receta>
    {
        Task<IEnumerable<object>> GetBeforeJune(int year);
    }
