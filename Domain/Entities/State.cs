using System;
using System.Collections.Generic;

namespace Domain.Entities;


public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CountryId { get; set; }
    public virtual Country? Country { get; set; }

    public ICollection<City> Cities {get;set;}
}
