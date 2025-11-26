using System.Security.Claims;
using LoanApi.Application.DTOs;
using LoanApi.Application.Interfaces;
using LoanApi.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyInfo()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetByIdAsync(userId);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpPost("{id}/block")]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> BlockUser(Guid id)
    {
        await _userService.BlockUserAsync(id);
        return Ok();
    }

    [HttpPost("{id}/unblock")]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> UnblockUser(Guid id)
    {
        await _userService.UnblockUserAsync(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> UpdateUser(Guid id, RegisterUserDto dto)
    {
        await _userService.UpdateUserAsync(id, dto);
        return Ok();
    }
}
