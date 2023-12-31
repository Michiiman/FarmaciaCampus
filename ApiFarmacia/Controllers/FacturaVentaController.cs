using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;

public class FacturaVentaController : BaseApiController
{
     private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public FacturaVentaController( IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<FacturaVentaDto>>> Get()
    {
        var entidad = await unitOfWork.FacturasVentas.GetAllAsync();
        return mapper.Map<List<FacturaVentaDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<FacturaVentaDto>> Get(int id)
    {
        var entidad = await unitOfWork.FacturasVentas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<FacturaVentaDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FacturaVenta>> Post(FacturaVentaDto entidadDto)
    {
        var entidad = this.mapper.Map<FacturaVenta>(entidadDto);
        this.unitOfWork.FacturasVentas.Add(entidad);
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
    public async Task<ActionResult<FacturaVentaDto>> Put(int id, [FromBody]FacturaVentaDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<FacturaVenta>(entidadDto);
        unitOfWork.FacturasVentas.Update(entidad);
        await unitOfWork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitOfWork.FacturasVentas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitOfWork.FacturasVentas.Remove(entidad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }

}
