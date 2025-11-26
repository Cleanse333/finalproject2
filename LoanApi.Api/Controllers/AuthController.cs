using LoanApi.Application.DTOs;
using LoanApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoanApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var response = await _authService.RegisterAsync(dto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        return Ok(response);
    }
}
