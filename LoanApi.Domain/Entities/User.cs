using LoanApi.Domain.Enums;

namespace LoanApi.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal MonthlyIncome { get; set; }
    public bool IsBlocked { get; set; }
    public UserRole Role { get; set; }
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
