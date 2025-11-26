using LoanApi.Application.DTOs;
using LoanApi.Domain.Entities;
using LoanApi.Domain.Enums;

namespace LoanApi.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task BlockUserAsync(Guid userId);
    Task UnblockUserAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
    Task UpdateUserAsync(Guid userId, RegisterUserDto dto);
}

public interface ILoanService
{
    Task<LoanDto> CreateLoanAsync(Guid userId, CreateLoanDto dto);
    Task<IEnumerable<LoanDto>> GetMyLoansAsync(Guid userId);
    Task<IEnumerable<LoanDto>> GetAllLoansAsync();
    Task UpdateLoanStatusAsync(Guid loanId, LoanStatus status);
    Task UpdateLoanAsync(Guid loanId, Guid userId, UpdateLoanDto dto);
    Task DeleteLoanAsync(Guid loanId, Guid userId);
}
