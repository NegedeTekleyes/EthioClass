using MediatR;
using EthioClass.Application.Common.Interfaces;
using EthioClass.Application.Users.Dtos;
using EthioClass.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EthioClass.Application.Users.Commands;

public class RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await context.Users
            .AnyAsync(u => u.SchoolId == request.SchoolId && u.Email == request.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException($"Email '{request.Email}' is already registered for this school.");

        var user = new User
        {
            SchoolId = request.SchoolId,
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHasher.Hash(request.Password),
            Role = request.Role
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return new UserResponseDto(user.Id, user.SchoolId, user.FullName, user.Email, user.Role, user.IsActive);
    }
}