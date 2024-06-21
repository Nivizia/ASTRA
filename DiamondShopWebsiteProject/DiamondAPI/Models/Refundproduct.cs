using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Refundproduct
{
    public Guid RfProductId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? ImageUrl { get; set; }

    public string? Specifications { get; set; }

    public double? CaratWeight { get; set; }

    public int? Color { get; set; }

    public int? Clarity { get; set; }

    public int? Cut { get; set; }

    public virtual ICollection<Diamondcertificate> Diamondcertificates { get; set; } = new List<Diamondcertificate>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
