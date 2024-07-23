using DiamondAPI.DTOs.VNPaymentResponse;
using DiamondAPI.Models;

namespace DiamondAPI.Mappers
{
    public static class VNPaymentResponseMappers
    {
        public static VnpaymentResponse ToVNPaymentResponse(this CreateVNPaymentResponseDTO createVNPaymentResponse)
        {
            return new VnpaymentResponse
            {
                ResponseId = Guid.NewGuid(),
                OrderId = createVNPaymentResponse.OrderId,
                Success = createVNPaymentResponse.Code switch
                {
                    "00" => true,
                    _ => false
                },
                Amount = createVNPaymentResponse.Amount,
                BankCode = createVNPaymentResponse.BankCode,
                BankTransactionNumber = createVNPaymentResponse.BankTransactionNumber,
                CardType = createVNPaymentResponse.CardType,
                OrderInfo = createVNPaymentResponse.OrderInfo,
                PaymentDate = createVNPaymentResponse.PaymentDate,
            };
        }
    }
}
