using DiamondAPI.DTOs.VNPaymentRequest;
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
        private readonly VNPayService _vnPayService;
        private readonly OrderRepository _orderRepo;

        public VNPayController(VNPayService vnPayService, OrderRepository orderRepo)
        {
            _vnPayService = vnPayService;
            _orderRepo = orderRepo;
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] CreateVNPaymentRequestDTO requestDTO)
        {
            var model = requestDTO.ToVnpaymentRequest();
            model.Amount = await _orderRepo.GetAmount(model.OrderId);
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, model);
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
