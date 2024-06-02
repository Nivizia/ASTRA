using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ringpairing
{
    public string RProductId { get; set; } = null!;

    public int? RingId { get; set; }

    public string? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Ring? Ring { get; set; }
}
