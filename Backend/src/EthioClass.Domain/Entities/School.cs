namespace EthioClass.Domain.Entities;

public class School
{
    public int Id { get; set; }

    // Basic Information
    public required string Name { get; set; }
    public required string NameAmharic { get; set; }
    public required string Code { get; set; }

    // Contact Information
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Website { get; set; }

    // Address
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }

    // Branding
    public string? LogoUrl { get; set; }
    public string? Motto { get; set; }

    // Status
    public bool IsActive { get; set; } = true;
    public DateTime? SubscriptionExpiresAt { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}