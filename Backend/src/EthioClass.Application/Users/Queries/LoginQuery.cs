using EthioClass.Application.Users.Dtos;
using MediatR;

namespace EthioClass.Application.Users.Queries;

public record LoginQuery(string SchoolCode, string Email, string Password): IRequest<LoginResponseDto>;