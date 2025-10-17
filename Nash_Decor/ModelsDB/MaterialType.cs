using System;
using System.Collections.Generic;

namespace Nash_Decor.ModelsDB;

public partial class MaterialType
{
    public int MaterialTypeId { get; set; }

    public string MaterialTypeName { get; set; } = null!;

    public decimal MaterialDefectPercentage { get; set; }

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
