
namespace ApiFarmacia.Dtos;

public class PersonaDto
{
    public int Id {get;set;}
    public string Nombre {get;set;}
    public string NumeroDocumento{get;set;}
    public string Direccion{get;set;}
    public TipoDocumentoDto TipoDocumento {get;set;}
    public TipoPersonaDto TipoPersona{get;set;}
}
