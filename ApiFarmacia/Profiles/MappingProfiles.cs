using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiFarmacia.Profiles;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {
        CreateMap<Persona,PersonaDto>().ReverseMap();
        CreateMap<TipoPersona,TipoPersonaDto>().ReverseMap();
        CreateMap<TipoDocumento,TipoDocumentoDto>().ReverseMap();
        CreateMap<Medicamento,MedicamentoDto>().ReverseMap();
        CreateMap<MedicamentoComprado,MedicamentoCompradoDto>().ReverseMap();
        CreateMap<MedicamentoVendido,MedicamentoVendidoDto>().ReverseMap();
        CreateMap<MedicamentoReceta,MedicamentoRecetaDto>().ReverseMap();
        CreateMap<Compra,CompraDto>().ReverseMap();
        CreateMap<MedicamentoComprado,MedicamentoCompradoDto>().ReverseMap();
        CreateMap<Receta,RecetaDto>().ReverseMap();
    }
}
