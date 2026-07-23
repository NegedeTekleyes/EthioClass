

namespace EthioClass.Application.Users.Dtos;

public record LoginResponseDto(string Token, int UserId, string FullName, string Role);