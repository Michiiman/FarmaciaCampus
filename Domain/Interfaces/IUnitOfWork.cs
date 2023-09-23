

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICompra Compras{ get; }
    IUser Users { get; }
    IRol Rols { get; }
    Task<int> SaveAsync();

}
