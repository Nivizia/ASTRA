using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Frametype
{
    public Guid FrameTypeId { get; set; }

    public string FrameTypeName { get; set; } = null!;

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();
}
