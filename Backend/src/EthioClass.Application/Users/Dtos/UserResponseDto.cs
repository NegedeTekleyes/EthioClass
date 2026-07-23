using EthioClass.Domain.Entities;

namespace EthioClass.Application.Users.Dtos;

public record UserResponseDto(int Id, int SchoolId, string FullName, string Email, Role Role, bool IsActive);