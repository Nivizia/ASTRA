using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Orderitem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public string? ProductId { get; set; }

    public decimal? Price { get; set; }

    public string? ProductType { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Diamond? Product { get; set; }

    public virtual Pendantpairing? Product1 { get; set; }

    public virtual Ringpairing? Product2 { get; set; }

    public virtual Refundproduct? Product3 { get; set; }

    public virtual Earringpairing? ProductNavigation { get; set; }

    public virtual Warranty? Warranty { get; set; }
}
