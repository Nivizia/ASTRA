using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ringpairing
{
    public int RProductId { get; set; }

    public int? RingId { get; set; }

    public int? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Ring? Ring { get; set; }
}
