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

    //metodos para endpoints
    [HttpGet("GetSalesPerEmployeed")]
    public async Task<ActionResult<IEnumerable<object>>> GetSalesPerEmployeed(int year)
    {
        var entidad = await unitOfWork.Personas.GetSalesPerEmployeed(year);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }

    [HttpGet("GetMoreThan5sales")]
    public async Task<ActionResult<IEnumerable<object>>> GetMoreThan5sales()
    {
        var entidad = await unitOfWork.Personas.GetMoreThan5sales();
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }

    [HttpGet("GetAnysales")]
    public async Task<ActionResult<IEnumerable<object>>> GetAnysales(int year)
    {
        var entidad = await unitOfWork.Personas.GetAnysales(year);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }

    [HttpGet("Get0To5sales")]
    public async Task<ActionResult<IEnumerable<object>>> Get0To5sales(int year)
    {
        var entidad = await unitOfWork.Personas.Get0To5sales(year);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }

    [HttpGet("GetBestSeller")]
    public async Task<ActionResult<object>> GetBestSeller(int year)
    {
        var entidad = await unitOfWork.Personas.GetBestSeller(year);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<object>(entidad));
    }

    [HttpGet("GetAnySalePerMonthNYear")]
    public async Task<ActionResult<IEnumerable<object>>> GetAnySalePerMonthNYear(int year, int month)
    {
        var entidad = await unitOfWork.Personas.GetAnySalePerMonthNYear(year, month);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }
}
