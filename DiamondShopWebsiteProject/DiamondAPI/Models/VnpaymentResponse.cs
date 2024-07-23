using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class VnpaymentResponse
{
    public Guid ResponseId { get; set; }

    public Guid OrderId { get; set; }

    public bool Success { get; set; }

    public decimal Amount { get; set; }

    public string? BankCode { get; set; }

    public string? BankTransactionNumber { get; set; }

    public string? CardType { get; set; }

    public string? OrderInfo { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
