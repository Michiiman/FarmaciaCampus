using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
public class RecetaController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public RecetaController( IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RecetaDto>>> Get()
    {
        var entidad = await unitOfWork.Recetas.GetAllAsync();
        return mapper.Map<List<RecetaDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecetaDto>> Get(int id)
    {
        var entidad = await unitOfWork.Recetas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<RecetaDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Receta>> Post(RecetaDto entidadDto)
    {
        var entidad = this.mapper.Map<Receta>(entidadDto);
        this.unitOfWork.Recetas.Add(entidad);
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
    public async Task<ActionResult<RecetaDto>> Put(int id, [FromBody]RecetaDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Receta>(entidadDto);
        unitOfWork.Recetas.Update(entidad);
        await unitOfWork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitOfWork.Recetas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitOfWork.Recetas.Remove(entidad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}