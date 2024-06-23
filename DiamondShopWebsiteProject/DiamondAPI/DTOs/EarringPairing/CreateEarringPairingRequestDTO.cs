namespace DiamondAPI.DTOs.EarringPairing
{
    public class CreateEarringPairingRequestDTO
    {
        public Guid? EarringId { get; set; }

        public Guid? DiamondId { get; set; }
    }
}
