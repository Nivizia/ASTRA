using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class VnpaymentRequest
{
    public Guid RequestId { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<VnpaymentResponse> VnpaymentResponses { get; set; } = new List<VnpaymentResponse>();
}
