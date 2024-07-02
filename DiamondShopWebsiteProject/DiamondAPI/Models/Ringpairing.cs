using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ringpairing
{
    public Guid RProductId { get; set; }

    public Guid? RingId { get; set; }

    public Guid? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual Ring? Ring { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
