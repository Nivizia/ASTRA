using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ringshapedetail
{
    public Guid RingShapeDetailId { get; set; }

    public Guid? RingId { get; set; }

    public Guid? ShapeId { get; set; }

    public string? ImageUrl { get; set; }

    public string? FrameDescription { get; set; }

    public virtual Ring? Ring { get; set; }

    public virtual Shape? Shape { get; set; }
}
