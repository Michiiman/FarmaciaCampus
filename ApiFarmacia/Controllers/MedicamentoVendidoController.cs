using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
public class MedicamentoVendidoController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public MedicamentoVendidoController( IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable< MedicamentoVendidoDto>>> Get()
    {
        var entidad = await unitOfWork.MedicamentosVendidos.GetAllAsync();
        return mapper.Map<List< MedicamentoVendidoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult< MedicamentoVendidoDto>> Get(int id)
    {
        var entidad = await unitOfWork.MedicamentosVendidos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map< MedicamentoVendidoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicamentoVendido>> Post( MedicamentoVendidoDto entidadDto)
    {
        var entidad = this.mapper.Map<MedicamentoVendido>(entidadDto);
        this.unitOfWork.MedicamentosVendidos.Add(entidad);
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
    public async Task<ActionResult< MedicamentoVendidoDto>> Put(int id, [FromBody] MedicamentoVendidoDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<MedicamentoVendido>(entidadDto);
        unitOfWork.MedicamentosVendidos.Update(entidad);
        await unitOfWork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitOfWork.MedicamentosVendidos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitOfWork.MedicamentosVendidos.Remove(entidad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }

    //controllers personalizadso para endpoints
    [HttpGet("Consulta5/{medicamento}")]
    public async Task<ActionResult<IEnumerable<object>>> GetVentasMedicamento(string medicamento)
    {
        var entidad = await unitOfWork.MedicamentosVendidos.GetVentasMedicamento(medicamento);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }

    [HttpGet("Consulta9")]
    public async Task<ActionResult<IEnumerable<object>>> GetNoSales()
    {
        var entidad = await unitOfWork.MedicamentosVendidos.GetNoSales();
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }
    
    [HttpGet("Consulta14")]
    public async Task<ActionResult<IEnumerable<object>>> GetSalesPerMounth (int year, int month)
    {
        var entidad = await unitOfWork.MedicamentosVendidos.GetSalesPerMounth (year, month);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }
}