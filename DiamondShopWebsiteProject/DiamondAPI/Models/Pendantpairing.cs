using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Pendantpairing
{
    public string PProductId { get; set; } = null!;

    public int? PendantId { get; set; }

    public string? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Pendant? Pendant { get; set; }
}
