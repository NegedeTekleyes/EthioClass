namespace EthioClass.Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    int? SchoolId { get; }
    string? Role { get; }
}