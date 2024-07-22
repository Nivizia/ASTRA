using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using DiamondAPI.Models;
using DiamondAPI.Utilities;

namespace DiamondAPI.Services
{
    public class VNPayService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<VNPayService> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public VNPayService(IConfiguration config, ILogger<VNPayService> logger, IHttpClientFactory factory)
        {
            _config = config;
            _logger = logger;
            _factory = factory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // Whoopsie daisy me took directly from model hehehaw
        public string CreatePaymentUrl(HttpContext context, VnpaymentRequest model)
        {
            var vnpay = new VNPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "deposit");
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", model.OrderId.ToString());

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
            _logger.LogInformation("create payment");
            return paymentUrl;
        }

        // Implement other methods (PaymentExecute, PaymentUpdateDatabase, GenerateQRCodeAsync, GetToken) similarly
    }
}
