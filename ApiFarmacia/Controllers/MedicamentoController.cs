using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
public class MedicamentoController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public MedicamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
    {
        var entidad = await unitOfWork.Medicamentos.GetAllAsync();
        return mapper.Map<List<MedicamentoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicamentoDto>> Get(int id)
    {
        var entidad = await unitOfWork.Medicamentos.GetByIdAsync(id);
        if (entidad == null)
        {
            return NotFound();
        }
        return this.mapper.Map<MedicamentoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicamento>> Post(MedicamentoDto entidadDto)
    {
        var entidad = this.mapper.Map<Medicamento>(entidadDto);
        this.unitOfWork.Medicamentos.Add(entidad);
        await unitOfWork.SaveAsync();
        if (entidad == null)
        {
            return BadRequest();
        }
        entidadDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new { id = entidadDto.Id }, entidadDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicamentoDto>> Put(int id, [FromBody] MedicamentoDto entidadDto)
    {
        if (entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Medicamento>(entidadDto);
        unitOfWork.Medicamentos.Update(entidad);
        await unitOfWork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entidad = await unitOfWork.Medicamentos.GetByIdAsync(id);
        if (entidad == null)
        {
            return NotFound();
        }
        unitOfWork.Medicamentos.Remove(entidad);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
    //metodos especificos para endpoints
    [HttpGet("Less50")]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get50Less()
    {
        var entidad = await unitOfWork.Medicamentos.GetLess50();
        return mapper.Map<List<MedicamentoDto>>(entidad);
    }

    [HttpGet("ByProveedorName")]
    public async Task<ActionResult<IEnumerable<object>>> GetProveedorName(string proveedor)
    {
        var entidad = await unitOfWork.Medicamentos.GetProveedorName(proveedor);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<object>>(entidad));
    }

    [HttpGet("AfterDueDate")]
    public async Task<ActionResult<IEnumerable<Medicamento>>> GetAfterDueDate(DateTime date)
    {
        var entidad = await unitOfWork.Medicamentos.GetAfterDate(date);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<MedicamentoDto>>(entidad));
    }

    [HttpGet("GetMostExpensive")]
    public async Task<ActionResult<MedicamentoDto>> GetMostExpensive()
    {
        var entidad = await unitOfWork.Medicamentos.GetMostExpensive();
        return this.mapper.Map<MedicamentoDto>(entidad);
    }

    [HttpGet("GetHighherPriceAndUnderStock")]
    public async Task<ActionResult<IEnumerable<Medicamento>>> GetHighherPrice_UnderStock(int price, int stock)
    {
        var entidad = await unitOfWork.Medicamentos.GetHighherPriceAndUnderStock(price, stock);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<MedicamentoDto>>(entidad));
    }

    [HttpGet("GetExpireYear")]
    public async Task<ActionResult<IEnumerable<Medicamento>>> GetExpireYear(int year)
    {
        var entidad = await unitOfWork.Medicamentos.GetExpireYear(year);
        if (entidad == null)
        {
            return NotFound();
        }
        return Ok(this.mapper.Map<IEnumerable<MedicamentoDto>>(entidad));
    }    
}