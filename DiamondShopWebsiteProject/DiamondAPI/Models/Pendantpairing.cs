using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Pendantpairing
{
    public Guid PProductId { get; set; }

    public Guid? PendantId { get; set; }

    public Guid? DiamondId { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual Pendant? Pendant { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
