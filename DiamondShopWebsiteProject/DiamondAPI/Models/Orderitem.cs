using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Orderitem
{
    public Guid OrderItemId { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? DiamondId { get; set; }

    public Guid? RingPairingId { get; set; }

    public Guid? EarringPairingId { get; set; }

    public Guid? PendantPairingId { get; set; }

    public decimal? Price { get; set; }

    public string? ProductType { get; set; }

    public virtual Diamond? Diamond { get; set; }

    public virtual Earringpairing? EarringPairing { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Pendantpairing? PendantPairing { get; set; }

    public virtual Ringpairing? RingPairing { get; set; }

    public virtual Warranty? Warranty { get; set; }
}
