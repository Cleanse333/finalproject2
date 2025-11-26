using LoanApi.Application.DTOs;
using LoanApi.Application.Interfaces;
using LoanApi.Domain.Entities;
using LoanApi.Domain.Enums;
using LoanApi.Domain.Interfaces;

namespace LoanApi.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IUserRepository _userRepository;

    public LoanService(ILoanRepository loanRepository, IUserRepository userRepository)
    {
        _loanRepository = loanRepository;
        _userRepository = userRepository;
    }

    public async Task<LoanDto> CreateLoanAsync(Guid userId, CreateLoanDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new Exception("User not found");
        if (user.IsBlocked) throw new Exception("User is blocked and cannot apply for a loan.");

        var loan = new Loan
        {
            UserId = userId,
            Type = dto.Type,
            Amount = dto.Amount,
            Currency = dto.Currency,
            Installment = dto.Installment,
            Status = LoanStatus.Processing,
            CreatedAt = DateTime.UtcNow
        };

        await _loanRepository.AddAsync(loan);

        return MapToDto(loan);
    }

    public async Task<IEnumerable<LoanDto>> GetMyLoansAsync(Guid userId)
    {
        var loans = await _loanRepository.GetByUserIdAsync(userId);
        return loans.Select(MapToDto);
    }

    public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
    {
        var loans = await _loanRepository.GetAllAsync();
        return loans.Select(MapToDto);
    }

    public async Task UpdateLoanStatusAsync(Guid loanId, LoanStatus status)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null) throw new Exception("Loan not found");

        loan.Status = status;
        await _loanRepository.UpdateAsync(loan);
    }

    public async Task UpdateLoanAsync(Guid loanId, Guid userId, UpdateLoanDto dto)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null) throw new Exception("Loan not found");
        if (loan.UserId != userId) throw new Exception("Unauthorized access to loan");
        if (loan.Status != LoanStatus.Processing) throw new Exception("Cannot update loan that is not in processing status");

        loan.Type = dto.Type;
        loan.Amount = dto.Amount;
        loan.Currency = dto.Currency;
        loan.Installment = dto.Installment;

        await _loanRepository.UpdateAsync(loan);
    }

    public async Task DeleteLoanAsync(Guid loanId, Guid userId)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null) throw new Exception("Loan not found");
        if (loan.UserId != userId) throw new Exception("Unauthorized access to loan");
        if (loan.Status != LoanStatus.Processing) throw new Exception("Cannot delete loan that is not in processing status");

        await _loanRepository.DeleteAsync(loan);
    }

    private static LoanDto MapToDto(Loan loan)
    {
        return new LoanDto
        {
            Id = loan.Id,
            UserId = loan.UserId,
            Type = loan.Type,
            Amount = loan.Amount,
            Currency = loan.Currency,
            Status = loan.Status,
            Installment = loan.Installment,
            CreatedAt = loan.CreatedAt
        };
    }
}
