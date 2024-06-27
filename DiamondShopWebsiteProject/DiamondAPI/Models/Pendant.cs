using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Pendant
{
    public Guid PendantId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public string? ChainType { get; set; }

    public string? ChainLength { get; set; }

    public string? ClaspType { get; set; }

    public virtual ICollection<Pendantpairing> Pendantpairings { get; set; } = new List<Pendantpairing>();
}
