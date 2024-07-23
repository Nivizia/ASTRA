using System.Text.Json;
using DiamondAPI.Models;
using DiamondAPI.Utilities;
using DiamondAPI.Repositories;
using DiamondAPI.Interfaces;

namespace DiamondAPI.Services
{
    public class VNPayService : IVNPayService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<VNPayService> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        // Removed unused _orderRepository field

        public VNPayService(IConfiguration config, ILogger<VNPayService> logger, IHttpClientFactory factory, IOrderRepository orderRepo)
        {
            _config = config;
            _logger = logger;
            _factory = factory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // _orderRepository initialization removed
        }

        public string CreatePaymentUrl(HttpContext context, VnpaymentRequest paymentRequest)
        {
            var vnpay = new VNPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"] ?? "defaultVersion");
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"] ?? "defaultCommand");
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"] ?? "defaultTmnCode");
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString("0"));
            vnpay.AddRequestData("vnp_CreateDate", paymentRequest.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"] ?? "defaultCurrCode");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"] ?? "defaultLocale");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng: " + paymentRequest.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:ReturnUrl"] ?? "http://astradiamonds.com:5212/order-confirmation");
            vnpay.AddRequestData("vnp_ExpireDate", paymentRequest.CreatedDate.AddHours(1).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_TxnRef", paymentRequest.OrderId.ToString());

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"] ?? "defaultBaseUrl", _config["VnPay:HashSecret"] ?? "defaultHashSecret");
            _logger.LogInformation("Payment URL created successfully.");
            return paymentUrl;
        }

        // Implement other methods (PaymentExecute, PaymentUpdateDatabase, GenerateQRCodeAsync, GetToken) similarly
    }
}
