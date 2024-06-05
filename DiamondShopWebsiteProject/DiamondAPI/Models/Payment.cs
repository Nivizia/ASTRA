using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid? OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentMessage { get; set; }

    public decimal? Amount { get; set; }

    public virtual Order? Order { get; set; }
}
