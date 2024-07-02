using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ringsubtype
{
    public Guid RingSubtypeId { get; set; }

    public Guid? RingTypeId { get; set; }

    public string SubtypeName { get; set; } = null!;

    public virtual Ringtype? RingType { get; set; }

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();
}
