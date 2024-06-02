using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Diamond
{
    public string DProductId { get; set; } = null!;

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? ImageUrl { get; set; }

    public string? DType { get; set; }

    public string? CaratWeight { get; set; }

    public string? Color { get; set; }

    public string? Clarity { get; set; }

    public string? Cut { get; set; }

    public virtual ICollection<Diamondcertificate> Diamondcertificates { get; set; } = new List<Diamondcertificate>();

    public virtual ICollection<Earringpairing> Earringpairings { get; set; } = new List<Earringpairing>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Pendantpairing> Pendantpairings { get; set; } = new List<Pendantpairing>();

    public virtual ICollection<Ringpairing> Ringpairings { get; set; } = new List<Ringpairing>();
}
