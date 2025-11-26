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
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var loan = await _loanService.CreateLoanAsync(userId, dto);
        return Ok(loan);
    }

    [HttpGet("my-loans")]
    public async Task<IActionResult> GetMyLoans()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var loans = await _loanService.GetMyLoansAsync(userId);
        return Ok(loans);
    }

    [HttpGet]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> GetAllLoans()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Accountant")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateLoanStatusDto dto)
    {
        await _loanService.UpdateLoanStatusAsync(id, dto.Status);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(Guid id, UpdateLoanDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _loanService.UpdateLoanAsync(id, userId, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _loanService.DeleteLoanAsync(id, userId);
        return Ok();
    }
}
