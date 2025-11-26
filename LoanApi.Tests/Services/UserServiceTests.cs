using LoanApi.Application.DTOs;
using LoanApi.Application.Services;
using LoanApi.Domain.Entities;
using LoanApi.Domain.Enums;
using LoanApi.Domain.Interfaces;
using Moq;
using Xunit;

namespace LoanApi.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };
        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

        var result = await _userService.GetByIdAsync(userId);

        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("testuser", result.Username);
    }

    [Fact]
    public async Task BlockUserAsync_ShouldBlockUser_WhenUserExists()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, IsBlocked = false };
        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

        await _userService.BlockUserAsync(userId);

        Assert.True(user.IsBlocked);
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }
}
