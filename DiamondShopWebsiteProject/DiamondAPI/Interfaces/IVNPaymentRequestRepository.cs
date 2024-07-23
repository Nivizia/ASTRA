using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IVNPaymentRequestRepository
    {
        public Task<VnpaymentRequest> CreateVNPaymentRequest(VnpaymentRequest vnpaymentRequest);

        public Task<Guid> GetPaymentId(Guid orderId);
    }
}
