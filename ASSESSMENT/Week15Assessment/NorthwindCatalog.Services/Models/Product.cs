using System;
using System.Collections.Generic;

namespace NorthwindCatalog.Services.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal UnitPrice { get; set; }

    public short UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }

    // this is the navigation property to the Category entity, which allows us to access the related Category data for a Product
    public virtual Category Category { get; set; } = null!;
}
