using LoanApi.Domain.Enums;

namespace LoanApi.Domain.Entities;

public class Loan
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public LoanType Type { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public LoanStatus Status { get; set; }
    public int Installment { get; set; } // Number of months? Or amount per month? Assuming number of months based on "installment" usually meaning the schedule. Or maybe "Period"? Prompt says "installment". I'll assume it means number of payments or period.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
