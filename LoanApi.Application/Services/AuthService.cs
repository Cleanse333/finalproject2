using LoanApi.Application.DTOs;
using LoanApi.Application.Interfaces;
using LoanApi.Domain.Entities;
using LoanApi.Domain.Enums;
using LoanApi.Domain.Interfaces;

namespace LoanApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
        {
            throw new Exceptions.ConflictException("Username already exists.");
        }
        
        var user = new User
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Username = dto.Username,
            Email = dto.Email,
            Age = dto.Age,
            MonthlyIncome = dto.MonthlyIncome,
            Role = dto.Role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepository.AddAsync(user);

        return new AuthResponseDto
        {
            Token = _jwtTokenGenerator.GenerateToken(user),
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Email = user.Email,
                Age = user.Age,
                MonthlyIncome = user.MonthlyIncome,
                IsBlocked = user.IsBlocked,
                Role = user.Role
            }
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null)
        {
            throw new Exception("Invalid username or password.");
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new Exception("Invalid username or password.");
        }

        if (user.IsBlocked)
        {
            throw new Exception("User is blocked.");
        }

        return new AuthResponseDto
        {
            Token = _jwtTokenGenerator.GenerateToken(user),
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Email = user.Email,
                Age = user.Age,
                MonthlyIncome = user.MonthlyIncome,
                IsBlocked = user.IsBlocked,
                Role = user.Role
            }
        };
    }
}
