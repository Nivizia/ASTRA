using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Earringpairing
{
    public int EProductId { get; set; }

    public int? EarringId { get; set; }

    public int? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual Earring? Earring { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
