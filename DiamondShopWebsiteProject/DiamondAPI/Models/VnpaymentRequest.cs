﻿using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class VnpaymentRequest
{
    public Guid PaymentId { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool Deposit { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual VnpaymentResponse? VnpaymentResponse { get; set; }
}
