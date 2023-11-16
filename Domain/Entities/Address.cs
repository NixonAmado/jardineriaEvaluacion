using System;
using System.Collections.Generic;

namespace Domain.Entities;


public partial class Address
{
    public int Id { get; set; }

    public string AdressLine1 { get; set; } = null!;

    public string AddressLine2 { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();
}
