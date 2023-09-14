namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public int IdUserFk {get;set;}
    public User user {get;set;}
    public string Token {get;set;}
    public DateTime expires {get;set;}
    public bool IsExpired => DateTime.UtcNow >= expires; 
    public DateTime Created {get;set;}
    public DateTime Revoked {get;set;}
    public bool IsActive => Revoked == null && !IsExpired;
}