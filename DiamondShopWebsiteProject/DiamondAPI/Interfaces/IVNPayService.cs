using DiamondAPI.Models;

namespace DiamondAPI.Interfaces
{
    public interface IVNPayService
    {
        public string CreatePaymentUrl(HttpContext context, VnpaymentRequest model);
    }
}
