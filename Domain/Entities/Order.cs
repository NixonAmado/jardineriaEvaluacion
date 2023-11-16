using System;
using System.Collections.Generic;

namespace Domain.Entities;


public partial class Order
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly ExpectedDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public string Comments { get; set; } = null!;

    public int? CustomerId { get; set; }

    public string? PaymentId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Payment? Payment { get; set; }
}
