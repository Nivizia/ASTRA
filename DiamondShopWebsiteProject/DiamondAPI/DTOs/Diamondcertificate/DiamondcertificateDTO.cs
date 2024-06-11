using DiamondAPI.DTOs.Diamond;

namespace DiamondAPI.DTOs.Diamondcertificate
{
    public class DiamondcertificateDTO
    {
        public Guid CertificateId { get; set; }

        public Guid? ProductId { get; set; }

        public string? CertificateNumber { get; set; }

        public string? IssuedBy { get; set; }

        public DateTime? IssueDate { get; set; }

        public virtual DiamondDTO? Product { get; set; }
    }
}
