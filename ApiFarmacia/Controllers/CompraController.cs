using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
public class CompraController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public CompraController( IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CompraDto>>> Get()
    {
        var entidad = await unitOfWork.Compras.GetAllAsync();
        return mapper.Map<List<CompraDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompraDto>> Get(int id)
    {
        var entidad = await unitOfWork.Compras.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<CompraDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Compra>> Post(CompraDto entidadDto)
    {
        var entidad = this.mapper.Map<Compra>(entidadDto);
        this.unitOfWork.Compras.Add(entidad);
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
    public async Task<ActionResult<CompraDto>> Put(int id, [FromBody]CompraDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Compra>(entidadDto);
        unitOfWork.Compras.Update(entidad);
        await unitOfWork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitOfWork.Compras.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitOfWork.Compras.Remove(entidad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }

}
