
using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly FarmaciaContext context;
    private CompraRepository _compras;
    private FacturaVentaRepository _facturaVenta;
    private MedicamentoRepository _medicamento;
    private MedicamentoCompradoRepository _medicamentoComprado;
    private MedicamentoRecetaRepository _medicamentoReceta;
    private MedicamentoVendidoRepository _medicamentoVendido;
    private PersonaRepository _persona;
    private RecetaRepository _receta;
    private TelefonoRepository _telefono;
    private TipoDocumentoRepository _tipoDocumento;
    private TipoPersonaRepository _tipoPersona;
    private UserRepository users;
    private RolRepository rols;

    public UnitOfWork(FarmaciaContext _context)
    {
        context = _context;
    }

    public ICompra Compras //Como se define en la IUnitOfWork
    {
        get
        {
            if (_compras == null)
            {
                _compras = new CompraRepository(context);
            }
            return _compras;
        }
    }

    public IMedicamento Medicamentos
    {
        get
        {
            if (_medicamento == null)
            {
                _medicamento = new MedicamentoRepository(context);
            }
            return _medicamento;
        }
    }

    public IFacturaVenta FacturasVentas
    {
        get
        {
            if (_facturaVenta == null)
            {
                _facturaVenta = new FacturaVentaRepository(context);
            }
            return _facturaVenta;
        }
    }
    public IMedicamentoComprado MedicamentosComprados
    {
        get
        {
            if (_medicamentoComprado == null)
            {
                _medicamentoComprado = new MedicamentoCompradoRepository(context);
            }
            return _medicamentoComprado;
        }
    }

    public IMedicamentoReceta MedicamentosRecetas
    {
        get
        {
            if (_medicamentoReceta == null)
            {
                _medicamentoReceta = new MedicamentoRecetaRepository(context);
            }
            return _medicamentoReceta;
        }
    }

    public IMedicamentoVendido MedicamentosVendidos
    {
        get
        {
            if (_medicamentoVendido == null)
            {
                _medicamentoVendido = new MedicamentoVendidoRepository(context);
            }
            return _medicamentoVendido;
        }
    }

    public IPersona Personas
    {
        get
        {
            if (_persona == null)
            {
                _persona = new PersonaRepository(context);
            }
            return _persona;
        }
    }

    public IReceta Recetas
    {
        get
        {
            if (_receta == null)
            {
                _receta = new RecetaRepository(context);
            }
            return _receta;
        }
    }

    public ITelefono Telefonos
    {
        get
        {
            if (_telefono == null)
            {
                _telefono = new TelefonoRepository(context);
            }
            return _telefono;
        }
    }

    public ITipoDocumento TiposDocumentos
    {
        get
        {
            if (_tipoDocumento == null)
            {
                _tipoDocumento = new TipoDocumentoRepository(context);
            }
            return _tipoDocumento;
        }
    }

    public ITipoPersona TiposPersonas
    {
        get
        {
            if (_tipoPersona == null)
            {
                _tipoPersona = new TipoPersonaRepository(context);
            }
            return _tipoPersona;
        }
    }

    public IUser Users
    {
        get
        {
            if (users == null)
            {
                users = new UserRepository(context);
            }
            return users;
        }
    }

    public IRol Rols
    {
        get{
            if(rols == null){
                rols = new RolRepository(context);
            }
            return rols;
        }
    }

    public int Save()
    {
        return context.SaveChanges();
    }
    public void Dispose()
    {
        context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}