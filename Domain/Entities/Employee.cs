namespace Domain.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName1 { get; set; } = null!;

    public string LastName2 { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string OfficeId { get; set; } = null!;

    public int? BossId { get; set; }

    public string JobTitle { get; set; } = null!;

    public virtual Employee Boss { get; set; } = null!;

    public virtual ICollection<Employee> InverseBoss { get; set; } = new List<Employee>();

    public virtual Office Office { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}