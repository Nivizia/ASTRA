namespace DiamondAPI.DTOs.Orderitem
{
    public class UpdateOrderitemRequestDTO
    {
        public Guid? OrderId { get; set; }

        public Guid? DiamondId { get; set; }

        public Guid? RingPairingId { get; set; }

        public Guid? EarringPairingId { get; set; }

        public Guid? PendantPairingId { get; set; }

        public decimal? Price { get; set; }

        public string? ProductType { get; set; }
    }
}
