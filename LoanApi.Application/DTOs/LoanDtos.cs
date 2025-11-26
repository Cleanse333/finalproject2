using LoanApi.Domain.Enums;

namespace LoanApi.Application.DTOs;

public class LoanDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public LoanType Type { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public LoanStatus Status { get; set; }
    public int Installment { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateLoanDto
{
    public LoanType Type { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public int Installment { get; set; }
}

public class UpdateLoanDto
{
    public LoanType Type { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public int Installment { get; set; }
}

public class UpdateLoanStatusDto
{
    public LoanStatus Status { get; set; }
}
