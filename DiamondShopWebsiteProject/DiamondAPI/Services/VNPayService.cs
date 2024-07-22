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
using DiamondAPI.Repositories;

namespace DiamondAPI.Services
{
    public class VNPayService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<VNPayService> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly OrderRepository _orderRepository;

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

        public string CreatePaymentUrl(HttpContext context, VnpaymentRequest model)
        {
            var vnpay = new VNPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString()); // Amount in VND * 100
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng: " + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "deposit"); // Use appropriate value
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", model.OrderId.ToString());

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
            _logger.LogInformation("Payment URL created successfully.");
            return paymentUrl;
        }
        /*
        public VnpaymentResponse PaymentExecute(IQueryCollection collections)
        {
            _logger.LogInformation("Payment execution started.");
            var vnpay = new VNPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            var vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount"));

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            string message;
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    message = "Payment successful. Thank you for choosing Diamond Jewelry.";
                    _logger.LogInformation($"Payment successful, OrderId={vnp_orderId}, VNPAY TranId={vnp_TransactionId}");
                }
                else
                {
                    message = $"Payment error. Error code: {vnp_ResponseCode}";
                    _logger.LogInformation($"Payment error, OrderId={vnp_orderId}, VNPAY TranId={vnp_TransactionId}, ResponseCode={vnp_ResponseCode}");
                }
            }
            else
            {
                _logger.LogInformation($"Invalid signature, InputData={collections}");
                message = "Payment processing error.";
            }

            return new VnpaymentResponse
            {
                Success = checkSignature && vnp_ResponseCode == "00" && vnp_TransactionStatus == "00",
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
                Amount = vnp_Amount,
                Message = message
            };
        }

        public async Task<(string StatusCode, string Message)> PaymentUpdateDatabase(IQueryCollection collections)
        {
            _logger.LogInformation("Payment update database started.");
            string returnContent;
            string message;

            if (collections.Count > 0)
            {
                var vnpay = new VNPayLibrary();
                foreach (var (key, value) in collections)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value.ToString());
                    }
                }

                var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
                var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
                var vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                var vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount"));

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
                if (checkSignature)
                {
                    var purchaseorder = await _orderRepository.GetOrderById(vnp_orderId);
                    if (purchaseorder == null)
                    {
                        returnContent = "{\"RspCode\":\"01\",\"Message\":\"Order not found\"}";
                        message = "Order not found";
                        _logger.LogInformation("Order not found.");
                    }
                    else
                    {
                        if (purchaseorder.OrderStatus == "Unpaid")
                        {
                            if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                            {
                                message = "Payment successful. Thank you for choosing Diamond Jewelry.";
                                await _purchaseOrderRepository.UpdatePurchaseOrderStatusAsync(purchaseorder.OrderId, "Paid");
                                _logger.LogInformation($"Payment successful, OrderId={vnp_orderId}, VNPAY TranId={vnp_TransactionId}");
                                returnContent = $"{{\"RspCode\":\"{vnp_ResponseCode}\",\"Message\":\"{message}\"}}";
                            }
                            else
                            {
                                message = $"Payment failed. Error code: {vnp_ResponseCode}";
                                await _purchaseOrderRepository.UpdatePurchaseOrderStatusAsync(purchaseorder.OrderId, "Failed");
                                _logger.LogInformation($"Payment error, OrderId={vnp_orderId}, VNPAY TranId={vnp_TransactionId}, ResponseCode={vnp_ResponseCode}");
                                returnContent = $"{{\"RspCode\":\"{vnp_ResponseCode}\",\"Message\":\"{message}\"}}";
                            }
                        }
                        else
                        {
                            returnContent = "{\"RspCode\":\"02\",\"Message\":\"Order already confirmed\"}";
                            message = "Order already confirmed";
                        }
                    }
                }
                else
                {
                    _logger.LogInformation($"Invalid signature, InputData={collections}");
                    returnContent = "{\"RspCode\":\"97\",\"Message\":\"Invalid signature\"}";
                    message = "Invalid signature";
                }
            }
            else
            {
                returnContent = "{\"RspCode\":\"99\",\"Message\":\"Input data required\"}";
                message = "Input data required";
            }

            return (returnContent, message);
        }*/

        // Implement other methods (PaymentExecute, PaymentUpdateDatabase, GenerateQRCodeAsync, GetToken) similarly
    }
}
