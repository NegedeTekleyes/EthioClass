using MediatR;
using EthioClass.Application.Schools.Dtos;

namespace EthioClass.Application.Schools.Commands;

public record CreateSchoolCommand(
    string Name,
    string NameAmharic,
    string Code,
    string? Address,
    string? City,
    string? PhoneNumber) : IRequest<SchoolResponseDto>;