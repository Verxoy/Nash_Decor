using System;
using System.Collections.Generic;

namespace Nash_Decor.ModelsDB;

public partial class EdIzmerenie
{
    public int UnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
