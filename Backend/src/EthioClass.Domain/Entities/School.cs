

namespace EthioClass.Domain.Entities;


public class School{
    public int Id {get; set;}
    public required string Name {get;set;}
    public required string NameAmharic {get; set;}
    public required string Code{get;set;}
    public string? Address{get; set; }
    public string? City{get; set; }
    public string? PhoneNumber{get; set; }
    public bool IsActive {get; set;} = true;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
}