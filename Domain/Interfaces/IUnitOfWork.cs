

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    //Se escriben en plural como se define en la unidad de trabajo
    ICompra Compras{ get; }
    IFacturaVenta FacturasVentas{ get; }
    IMedicamento Medicamentos{ get; }
    IMedicamentoComprado MedicamentosComprados{ get; }
    IMedicamentoVendido MedicamentosVendidos{ get; }
    IMedicamentoReceta MedicamentosRecetas{ get; }
    IPersona Personas{ get; }
    IReceta Recetas{ get; }
    ITelefono Telefonos{ get; }
    ITipoDocumento TiposDocumentos{ get; }
    ITipoPersona TiposPersonas{ get; }
    IUser Users { get; }
    IRol Rols { get; }
    Task<int> SaveAsync();

}
