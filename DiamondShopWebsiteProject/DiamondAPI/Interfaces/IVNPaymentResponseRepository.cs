using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IVNPaymentResponseRepository
    {
        public Task<VnpaymentResponse> CreateVNPaymentResponse(VnpaymentResponse vnpaymentResponse);

        public Task<bool> PaymentResponseExists(Guid paymentId);
    }
}
