using LoanApi.Application.DTOs;
using LoanApi.Application.Services;
using LoanApi.Domain.Entities;
using LoanApi.Domain.Enums;
using LoanApi.Domain.Interfaces;
using Moq;
using Xunit;

namespace LoanApi.Tests.Services;

public class LoanServiceTests
{
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly LoanService _loanService;

    public LoanServiceTests()
    {
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _loanService = new LoanService(_loanRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateLoanAsync_ShouldCreateLoan_WhenUserIsNotBlocked()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, IsBlocked = false };
        var dto = new CreateLoanDto { Amount = 1000, Currency = Currency.USD, Type = LoanType.FastLoan, Installment = 12 };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

        var result = await _loanService.CreateLoanAsync(userId, dto);

        Assert.NotNull(result);
        Assert.Equal(1000, result.Amount);
        Assert.Equal(LoanStatus.Processing, result.Status);
        _loanRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Loan>()), Times.Once);
    }

    [Fact]
    public async Task CreateLoanAsync_ShouldThrowException_WhenUserIsBlocked()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, IsBlocked = true };
        var dto = new CreateLoanDto { Amount = 1000 };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

        await Assert.ThrowsAsync<Exception>(() => _loanService.CreateLoanAsync(userId, dto));
    }
}
