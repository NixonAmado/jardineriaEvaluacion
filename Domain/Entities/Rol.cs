namespace Domain.Entities;

public class Rol : BaseEntity
{
    public string Description {get;set;}
    public ICollection<Rol> Rols {get;set;} = new HashSet<Rol>();
    public ICollection<UserRol> UsersRols {get;set;}
}