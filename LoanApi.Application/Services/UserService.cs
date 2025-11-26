using LoanApi.Application.DTOs;
using LoanApi.Application.Interfaces;
using LoanApi.Domain.Interfaces;

namespace LoanApi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserDto
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
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new UserDto
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
        });
    }

    public async Task BlockUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        user.IsBlocked = true;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UnblockUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        user.IsBlocked = false;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        await _userRepository.DeleteAsync(user);
    }

    public async Task UpdateUserAsync(Guid userId, RegisterUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        user.Name = dto.Name;
        user.Surname = dto.Surname;
        user.Email = dto.Email;
        user.Age = dto.Age;
        user.MonthlyIncome = dto.MonthlyIncome;
        await _userRepository.UpdateAsync(user);
    }
}
