using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class VnpaymentResponse
{
    public Guid ResponseId { get; set; }

    public Guid RequestId { get; set; }

    public bool Success { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderDescription { get; set; }

    public string? OrderId { get; set; }

    public string? TransactionId { get; set; }

    public string? Token { get; set; }

    public string? VnPayResponseCode { get; set; }

    public decimal? Amount { get; set; }

    public string? Message { get; set; }

    public DateTime ResponseDate { get; set; }

    public virtual VnpaymentRequest Request { get; set; } = null!;
}
