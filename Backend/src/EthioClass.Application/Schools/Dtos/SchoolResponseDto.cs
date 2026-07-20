namespace EthioClass.Application.Schools.Dtos;

public record SchoolResponseDto(
    int Id,
    string Name,
    string NameAmharic,
    string Code,
    string? Email,
    string? PhoneNumber,
    string? Address,
    string? City,
    string? Region,
    bool IsActive);