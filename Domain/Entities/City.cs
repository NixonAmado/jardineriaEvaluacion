namespace Domain.Entities;


public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
