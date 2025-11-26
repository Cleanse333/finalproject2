using FluentValidation;
using LoanApi.Application.DTOs;

namespace LoanApi.Application.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(20);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Age).GreaterThan(18);
        RuleFor(x => x.MonthlyIncome).GreaterThan(0);
    }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
