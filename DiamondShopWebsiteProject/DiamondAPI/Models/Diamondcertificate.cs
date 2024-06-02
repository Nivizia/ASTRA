using System;
using System.Collections.Generic;

namespace DiamondAPI.Models;

public partial class Diamondcertificate
{
    public int CertificateId { get; set; }

    public string? ProductId { get; set; }

    public string? CertificateNumber { get; set; }

    public string? IssuedBy { get; set; }

    public DateTime? IssueDate { get; set; }

    public virtual Diamond? Product { get; set; }

    public virtual Refundproduct? ProductNavigation { get; set; }
}
