﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;


public partial class Office
{
    public string Id { get; set; } = null!;

    public int? AddressId { get; set; }

    public string? Phone { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
