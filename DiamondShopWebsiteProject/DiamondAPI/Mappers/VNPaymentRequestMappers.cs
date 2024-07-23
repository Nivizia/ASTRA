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
                PaymentId = Guid.NewGuid(),
                OrderId = createVNPaymentRequest.OrderId,
                Deposit = createVNPaymentRequest.IsDeposit,
            };
        }
    }
}
