using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Shape
{
    public Guid ShapeId { get; set; }

    public string ShapeName { get; set; } = null!;

    public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();

    public virtual ICollection<Ringshapedetail> Ringshapedetails { get; set; } = new List<Ringshapedetail>();
}
