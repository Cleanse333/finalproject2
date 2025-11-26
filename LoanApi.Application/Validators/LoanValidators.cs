using FluentValidation;
using LoanApi.Application.DTOs;

namespace LoanApi.Application.Validators;

public class CreateLoanDtoValidator : AbstractValidator<CreateLoanDto>
{
    public CreateLoanDtoValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Installment).GreaterThan(0);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Currency).IsInEnum();
    }
}

public class UpdateLoanDtoValidator : AbstractValidator<UpdateLoanDto>
{
    public UpdateLoanDtoValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Installment).GreaterThan(0);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Currency).IsInEnum();
    }
}
