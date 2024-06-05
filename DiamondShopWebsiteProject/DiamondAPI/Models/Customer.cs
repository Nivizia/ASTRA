using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Shippingaddress> Shippingaddresses { get; set; } = new List<Shippingaddress>();
}
