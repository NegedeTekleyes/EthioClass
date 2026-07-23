using EthioClass.Application.Users.Dtos;
using EthioClass.Domain.Entities;
using MediatR;

namespace EthioClass.Application.Users.Commands;

public record RegisterUserCommand(
    int SchoolId,
    string FullName,
    string Email,
    string Password,
    Role Role): IRequest<UserResponseDto>;