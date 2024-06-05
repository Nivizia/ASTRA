using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Warranty
{
    public Guid WarrantyId { get; set; }

    public Guid? OrderItemId { get; set; }

    public DateTime? WarrantyStartDate { get; set; }

    public DateTime? WarrantyEndDate { get; set; }

    public virtual Orderitem? OrderItem { get; set; }
}
