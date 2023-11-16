namespace Domain.Entities;


public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StateId {get;set;}
    public State State {get;set;}
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    
}
