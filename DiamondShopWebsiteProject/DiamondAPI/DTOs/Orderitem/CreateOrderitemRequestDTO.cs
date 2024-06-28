using DiamondAPI.DTOs.EarringPairing;
using DiamondAPI.DTOs.Orderitem;
using DiamondAPI.DTOs.PendantPairing;
using DiamondAPI.DTOs.RingPairing;

namespace DiamondAPI.DTOs.Order
{
    public class CreateOrderitemRequestDTO
    {
        public Guid? ProductId { get; set; }

        public decimal? Price { get; set; }

        public string? ProductType { get; set; }

        public CreateRingPairingRequestDTO? CreateRingPairingDTO { get; set; }

        public CreatePendantPairingRequestDTO? CreatePendantPairingDTO { get; set; }

        public CreateEarringPairingRequestDTO? CreateEarringPairingDTO { get; set; }
    }
}