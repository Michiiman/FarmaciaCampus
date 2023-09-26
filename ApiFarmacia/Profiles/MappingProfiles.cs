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
    }
}
