using LoanApi.Domain.Entities;
using LoanApi.Domain.Enums;

namespace LoanApi.Domain.Interfaces;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id);
    Task<IEnumerable<Loan>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Loan>> GetAllAsync();
    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task DeleteAsync(Loan loan);
}
