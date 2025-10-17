using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nash_Decor.ModelsDB;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductTypeId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Article { get; set; } = null!;

    public decimal MinPartnerPrice { get; set; }

    public decimal RollWidth { get; set; }

    public virtual ProductType ProductType { get; set; } = null!;
    
    
}

