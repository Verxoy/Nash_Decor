using System;
using System.Collections.Generic;

namespace Nash_Decor.ModelsDB;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public int MaterialTypeId { get; set; }

    public decimal UnitPrice { get; set; }

    public int StockQuantity { get; set; }

    public int MinQuantity { get; set; }

    public int QuantityPerPackage { get; set; }

    public int UnitId { get; set; }

    public virtual MaterialType MaterialType { get; set; } = null!;

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();

    public virtual EdIzmerenie Unit { get; set; } = null!;
}
