using DiamondAPI.DTOs.VNPaymentRequest;
using DiamondAPI.DTOs.VNPaymentResponse;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using DiamondAPI.Repositories;
using DiamondAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/VNPay")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayService _vnPayService;
        private readonly IOrderRepository _orderRepo;
        private readonly IVNPaymentRequestRepository _vnPaymentRequestRepo;
        private readonly IVNPaymentResponseRepository _vnPaymentResponseRepo;

        public VNPayController(IVNPayService vnPayService, IOrderRepository orderRepo, IVNPaymentRequestRepository vNPaymentRequestRepo, IVNPaymentResponseRepository vNPaymentResponseRepo)
        {
            _vnPayService = vnPayService;
            _orderRepo = orderRepo;
            _vnPaymentRequestRepo = vNPaymentRequestRepo;
            _vnPaymentResponseRepo = vNPaymentResponseRepo;
        }

        [HttpPost("create-payment-url")]
        [Route("Deposit")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] CreateVNPaymentRequestDTO requestDTO, [FromRoute] bool Deposit)
        {
            var paymentRequest = requestDTO.ToVnpaymentRequest();
            paymentRequest.Amount = await _orderRepo.GetAmount(paymentRequest.OrderId);

            if (Deposit)
            {
                paymentRequest.Amount *= (decimal)0.4;
            }

            paymentRequest.CreatedDate = await _orderRepo.GetOrderDate(paymentRequest.OrderId);
            await _vnPaymentRequestRepo.CreateVNPaymentRequest(paymentRequest);
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, paymentRequest, Deposit);
            return Ok(new { paymentUrl });
        }

        [HttpGet("return")]
        public async Task<IActionResult> PaymentReturn([FromBody] CreateVNPaymentResponseDTO createVNPaymentResponseDTO)
        {
            var response = Request.Query;
            await _vnPaymentResponseRepo.CreateVNPaymentResponse(createVNPaymentResponseDTO.ToVNPaymentResponse());

            var orderId = await _vnPaymentRequestRepo.GetOrderRequest(createVNPaymentResponseDTO.RequestId);
            await _orderRepo.UpdateOrderStatus(orderId, "Deposit Received");
            return Ok("Payment processed successfully");
        }
    }
}
