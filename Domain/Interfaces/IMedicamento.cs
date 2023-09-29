using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento : IGenericRepo<Medicamento>
    {
        Task<IEnumerable<Medicamento>>GetMedicamentoMenosDe50(int cantidad);
    }
