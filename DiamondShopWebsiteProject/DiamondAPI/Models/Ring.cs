using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ring
{
    public int RingId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public string? MetalType { get; set; }

    public string? RingSize { get; set; }

    public virtual ICollection<Ringpairing> Ringpairings { get; set; } = new List<Ringpairing>();
}
