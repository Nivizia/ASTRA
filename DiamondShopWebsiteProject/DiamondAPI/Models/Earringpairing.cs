using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Earringpairing
{
    public string EProductId { get; set; } = null!;

    public int? EarringId { get; set; }

    public string? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual Earring? Earring { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
