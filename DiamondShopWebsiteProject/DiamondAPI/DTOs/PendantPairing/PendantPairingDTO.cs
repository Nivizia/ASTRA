namespace DiamondAPI.DTOs.PendantPairing
{
    public class PendantPairingDTO
    {
        public Guid PProductId { get; set; }

        public Guid? PendantId { get; set; }

        public Guid? DiamondId { get; set; }
    }
}
