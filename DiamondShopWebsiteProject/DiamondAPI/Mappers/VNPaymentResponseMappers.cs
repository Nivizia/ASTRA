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
                OrderId = createVNPaymentResponse.OrderId,
                Success = createVNPaymentResponse.ResponseCode == "00" && createVNPaymentResponse.TransactionStatus == "00",
                Amount = createVNPaymentResponse.Amount ?? 0m,
                BankCode = createVNPaymentResponse.BankCode,
                BankTransactionNumber = createVNPaymentResponse.BankTransactionNumber ?? null,
                CardType = createVNPaymentResponse.CardType,
                OrderInfo = createVNPaymentResponse.OrderInfo,
                PaymentDate = createVNPaymentResponse.PaymentDate,
            };
        }
    }
}
