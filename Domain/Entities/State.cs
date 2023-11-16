using System;
using System.Collections.Generic;

namespace Domain.Entities;


public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
