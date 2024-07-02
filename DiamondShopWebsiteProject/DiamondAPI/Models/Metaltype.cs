using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Metaltype
{
    public Guid MetalTypeId { get; set; }

    public string MetalTypeName { get; set; } = null!;

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();
}
