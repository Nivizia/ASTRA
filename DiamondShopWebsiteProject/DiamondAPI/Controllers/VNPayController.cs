using DiamondAPI.DTOs.VNPaymentRequest;
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

        public VNPayController(IVNPayService vnPayService, IOrderRepository orderRepo, IVNPaymentRequestRepository vNPaymentRequestRepo)
        {
            _vnPayService = vnPayService;
            _orderRepo = orderRepo;
            _vnPaymentRequestRepo = vNPaymentRequestRepo;
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] CreateVNPaymentRequestDTO requestDTO)
        {
            var paymentRequest = requestDTO.ToVnpaymentRequest();
            paymentRequest.Amount = await _orderRepo.GetAmount(paymentRequest.OrderId);
            paymentRequest.CreatedDate = await _orderRepo.GetOrderDate(paymentRequest.OrderId);
            await _vnPaymentRequestRepo.CreateVNPaymentRequest(paymentRequest);
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, paymentRequest);
            return Ok(new { paymentUrl });
        }

        [HttpGet("return")]
        public IActionResult PaymentReturn()
        {
            // Handle the return from VNPay, validate the response, and update the order status
            var response = Request.Query;
            // Validate and process the response here
            return Ok("Payment processed successfully");
        }
    }
}
