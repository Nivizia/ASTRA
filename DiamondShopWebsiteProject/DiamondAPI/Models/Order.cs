using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? OrderFirstName { get; set; }

    public string? OrderLastName { get; set; }

    public string? OrderEmail { get; set; }

    public string? OrderPhone { get; set; }

    public string? OrderStatus { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<VnpaymentRequest> VnpaymentRequests { get; set; } = new List<VnpaymentRequest>();

    public virtual ICollection<VnpaymentResponse> VnpaymentResponses { get; set; } = new List<VnpaymentResponse>();
}
