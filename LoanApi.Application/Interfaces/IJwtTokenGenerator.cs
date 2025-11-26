using LoanApi.Domain.Entities;

namespace LoanApi.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
