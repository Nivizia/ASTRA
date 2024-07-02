using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Stonecut
{
    public Guid StoneCutId { get; set; }

    public string StoneCutName { get; set; } = null!;

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();
}
