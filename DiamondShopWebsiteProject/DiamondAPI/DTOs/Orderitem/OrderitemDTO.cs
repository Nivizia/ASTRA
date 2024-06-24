using DiamondAPI.DTOs.Pendant;
using DiamondAPI.DTOs.PendantPairing;
using DiamondAPI.DTOs.RingPairing;

namespace DiamondAPI.DTOs.Orderitem
{
    public class OrderitemDTO
    {
        public Guid? OrderId { get; set; }

        public Guid? ProductId { get; set; }

        public decimal? Price { get; set; }

        public string? ProductType { get; set; }

        public CreateRingPairingRequestDTO? CreateRingPairingDTO { get; set; }

        public CreatePendantPairingRequestDTO? CreatePendantPairingDTO { get; set; }
    }
}