namespace DiamondAPI.DTOs.PendantPairing
{
    public class CreatePendantPairingRequestDTO
    {
        public Guid? PendantId { get; set; }

        public Guid? DiamondId { get; set; }
    }
}
