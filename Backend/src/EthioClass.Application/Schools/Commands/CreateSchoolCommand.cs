using MediatR;
using EthioClass.Application.Schools.Dtos;

namespace EthioClass.Application.Schools.Commands;

public record CreateSchoolCommand(
    string Name,
    string NameAmharic,
    string Code,
    string? Email,
    string? PhoneNumber,
    string? Address,
    string? City,
    string? Region,
    string? Website,
    string? Motto) : IRequest<SchoolResponseDto>;