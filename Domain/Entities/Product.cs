using System;
using System.Collections.Generic;

namespace Domain.Entities;
public class Product
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Gama { get; set; }

    public string Supplier { get; set; } = null!;

    public string Dimentions { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int StockQuantity { get; set; }

    public decimal SalePrice { get; set; }

    public decimal SupplierPrice { get; set; }

    public virtual ProductGama? GamaNavigation { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
}
