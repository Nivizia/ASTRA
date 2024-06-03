using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Pendantpairing
{
    public int PProductId { get; set; }

    public int? PendantId { get; set; }

    public int? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Pendant? Pendant { get; set; }
}
