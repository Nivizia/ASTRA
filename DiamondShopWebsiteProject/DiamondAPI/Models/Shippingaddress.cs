using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Shippingaddress
{
    public int AddressId { get; set; }

    public int? CustomerId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public virtual Customer? Customer { get; set; }
}
