namespace EthioClass.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public required int SchoolId { get; set; }

    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required Role Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public School School { get; set; } = null!;
}