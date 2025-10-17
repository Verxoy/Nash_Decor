using System;
using System.Collections.Generic;

namespace Nash_Decor.ModelsDB;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string ProductTypeName { get; set; } = null!;

    public decimal ProductTypeCoefficient { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
