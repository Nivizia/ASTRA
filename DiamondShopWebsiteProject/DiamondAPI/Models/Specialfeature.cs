using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Specialfeature
{
    public Guid SpecialFeatureId { get; set; }

    public string FeatureDescription { get; set; } = null!;

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();
}
