namespace DiamondAPI.DTOs.Diamondcertificate
{
    public class CreateDiamondcertificateRequestDTO
    {
        public Guid? ProductId { get; set; }

        public string? CertificateNumber { get; set; }

        public string? IssuedBy { get; set; }

        public DateTime? IssueDate { get; set; }
    }
}
