using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;

public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public PersonaController( IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var entidad = await unitOfWork.Personas.GetAllAsync();
        return mapper.Map<List<PersonaDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PersonaDto>> Get(int id)
    {
        var entidad = await unitOfWork.Personas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<PersonaDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(PersonaDto entidadDto)
    {
        var entidad = this.mapper.Map<Persona>(entidadDto);
        this.unitOfWork.Personas.Add(entidad);
        await unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        entidadDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = entidadDto.Id}, entidadDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody]PersonaDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Persona>(entidadDto);
        unitOfWork.Personas.Update(entidad);
        await unitOfWork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitOfWork.Personas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitOfWork.Personas.Remove(entidad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }

    
    [HttpGet("consulta12/{data}")]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetPacientesCompraronParacetamol(string data)
    {
        var entidad = await unitOfWork.Personas.GetPacientesCompraronParacetamol(data);
        return mapper.Map<List<PersonaDto>>(entidad);
    }

    [HttpGet("consulta25")]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetPacientesCompraronParacetamolEn2023(string data,int año)
    {
        var entidad = await unitOfWork.Personas.GetPacientesCompraronParacetamolEn2023(data,año);
        return mapper.Map<List<PersonaDto>>(entidad);
    }

    [HttpGet("consulta22/{año}")]
    public async Task<ActionResult<object>> GetPersonaQueMasComproEnAño(int año)
    {
        var entidad = await unitOfWork.Personas.GetPersonaQueMasComproEnAño(año);
        var dto = mapper.Map<object>(entidad); // Mapear PersonaCompraDto a object
        return Ok(dto);
    }
    [HttpGet("consulta30/{año}")]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetPersonaQueNoHaComprado2023(int año)
    {
        var entidad = await unitOfWork.Personas.GetPersonaQueNoHaComprado2023(año);
        return mapper.Map<List<PersonaDto>>(entidad);
    }

    [HttpGet("consulta33/{año}")]
public async Task<ActionResult<IEnumerable<object>>> GetTotalGastadoPorPacienteEnAño(int año)
{
    var entidad = await unitOfWork.Personas.GetTotalGastadoPorPacienteEnAño(año);
    var dto = mapper.Map<object>(entidad); // Mapear PersonaCompraDto a object
    return Ok(dto);
}

}
