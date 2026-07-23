using MediatR;
using Microsoft.EntityFrameworkCore;
using EthioClass.Application.Common.Interfaces;
using EthioClass.Application.Users.Dtos;

namespace EthioClass.Application.Users.Queries;

public class LoginQueryHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginQuery, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var school = await context.Schools
            .FirstOrDefaultAsync(s => s.Code == request.SchoolCode, cancellationToken);

        if (school is null)
            throw new InvalidOperationException("Invalid school code, email, or password.");

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.SchoolId == school.Id && u.Email == request.Email, cancellationToken);

        if (user is null || !user.IsActive)
            throw new InvalidOperationException("Invalid school code, email, or password.");

        var passwordMatches = passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!passwordMatches)
            throw new InvalidOperationException("Invalid school code, email, or password.");

        var token = jwtTokenGenerator.GenerateToken(user);

        return new LoginResponseDto(token, user.Id, user.FullName, user.Role.ToString());
    }
}