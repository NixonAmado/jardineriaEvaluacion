namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name {get;set;}
    public string Email{get;set;}
    public string Password{get;set;}
    public ICollection<User> Users {get;set;} = new HashSet<User>();
    public ICollection<UserRol> UsersRols {get;set;}

}