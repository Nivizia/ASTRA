using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ring
{
    public Guid RingId { get; set; }

    public Guid? RingTypeId { get; set; }

    public Guid? RingSubtypeId { get; set; }

    public Guid? FrameTypeId { get; set; }

    public Guid? MetalTypeId { get; set; }

    public string? RingSize { get; set; }

    public string? RingName { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Frametype? FrameType { get; set; }

    public virtual Metaltype? MetalType { get; set; }

    public virtual Ringsubtype? RingSubtype { get; set; }

    public virtual Ringtype? RingType { get; set; }

    public virtual ICollection<Ringpairing> Ringpairings { get; set; } = new List<Ringpairing>();

    public virtual ICollection<Ringshapedetail> Ringshapedetails { get; set; } = new List<Ringshapedetail>();
}
