using DiamondAPI.DTOs.VNPaymentRequest;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class VNPaymentRequestMappers
    {
        public static VnpaymentRequest ToVnpaymentRequest(this CreateVNPaymentRequestDTO createVNPaymentRequest)
        {
            return new VnpaymentRequest
            {
                RequestId = Guid.NewGuid(),
                OrderId = createVNPaymentRequest.OrderId,
            };
        }
    }
}
