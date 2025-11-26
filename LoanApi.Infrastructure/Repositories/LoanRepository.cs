using LoanApi.Domain.Entities;
using LoanApi.Domain.Interfaces;
using LoanApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LoanApi.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LoanDbContext _context;

    public LoanRepository(LoanDbContext context)
    {
        _context = context;
    }

    public async Task<Loan?> GetByIdAsync(Guid id)
    {
        return await _context.Loans.Include(l => l.User).FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Loan>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Loans.Where(l => l.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetAllAsync()
    {
        return await _context.Loans.Include(l => l.User).ToListAsync();
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Loan loan)
    {
        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
    }
}
