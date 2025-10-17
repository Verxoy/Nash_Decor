using System;
using System.Collections.Generic;

namespace Nash_Decor.ModelsDB;

public partial class ProductMaterial
{
    public int ProductMaterialId { get; set; }

    public string ProductName { get; set; } = null!;

    public int MaterialId { get; set; }

    public decimal RequiredQuantity { get; set; }

    public virtual Material Material { get; set; } = null!;
}
