using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Ringtype
{
    public Guid RingTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();

    public virtual ICollection<Ringsubtype> Ringsubtypes { get; set; } = new List<Ringsubtype>();
}
