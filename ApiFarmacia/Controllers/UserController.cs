using ApiFarmacia.Dtos;
using ApiFarmacia.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;


namespace ApiFarmacia.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DataUserDto>>> Get()
    {
        var entidad = await unitOfWork.Recetas.GetAllAsync();
        return mapper.Map<List<DataUserDto>>(entidad);
    }
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }

}
