using LoanApi.Domain.Enums;

namespace LoanApi.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal MonthlyIncome { get; set; }
    public bool IsBlocked { get; set; }
    public UserRole Role { get; set; }
}

public class RegisterUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal MonthlyIncome { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}

public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
