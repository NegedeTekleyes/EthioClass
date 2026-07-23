using EthioClass.Domain.Entities;

namespace EthioClass.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}