using DiamondAPI.DTOs.VNPaymentRequest;
using DiamondAPI.DTOs.VNPaymentResponse;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using DiamondAPI.Repositories;
using DiamondAPI.Services;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/VNPay")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayService _vnPayService;
        private readonly OrderService _orderService;
        private readonly IOrderRepository _orderRepo;
        private readonly IVNPaymentRequestRepository _vnPaymentRequestRepo;
        private readonly IVNPaymentResponseRepository _vnPaymentResponseRepo;

        public VNPayController(IVNPayService vnPayService, IOrderRepository orderRepo, IVNPaymentRequestRepository vNPaymentRequestRepo, IVNPaymentResponseRepository vNPaymentResponseRepo, OrderService orderService)
        {
            _vnPayService = vnPayService;
            _orderRepo = orderRepo;
            _vnPaymentRequestRepo = vNPaymentRequestRepo;
            _vnPaymentResponseRepo = vNPaymentResponseRepo;
            _orderService = orderService;
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] CreateVNPaymentRequestDTO requestDTO)
        {
            var paymentRequest = requestDTO.ToVnpaymentRequest();
            paymentRequest.Amount = await _orderRepo.GetAmount(paymentRequest.OrderId);
            paymentRequest.CreatedDate = await _orderRepo.GetOrderDate(paymentRequest.OrderId);

            if (requestDTO.IsDeposit)
            {
                paymentRequest.Amount *= (decimal)0.4;
            }
            await _vnPaymentRequestRepo.CreateVNPaymentRequest(paymentRequest);
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, paymentRequest, requestDTO.IsDeposit);
            return Ok(new { paymentUrl });
        }

        [HttpPost("return")]
        public async Task<IActionResult> PaymentReturn([FromBody] CreateVNPaymentResponseDTO createVNPaymentResponseDTO)
        {
            var VNPaymentResponseModel = createVNPaymentResponseDTO.ToVNPaymentResponse();
            VNPaymentResponseModel.PaymentId = await _vnPaymentRequestRepo.GetPaymentId(createVNPaymentResponseDTO.OrderId);

            if (await _vnPaymentResponseRepo.PaymentResponseExists(VNPaymentResponseModel.PaymentId))
            {
                return BadRequest("Payment response already exists.");
            }

            if (VNPaymentResponseModel.Success)
            {
                await _orderRepo.UpdateOrderStatus(createVNPaymentResponseDTO.OrderId, "Payment Received");
                await _vnPaymentResponseRepo.CreateVNPaymentResponse(VNPaymentResponseModel);
                return Ok("Payment processed successfully");
            }
            else
            {
                await _orderRepo.UpdateOrderStatus(createVNPaymentResponseDTO.OrderId, "Payment Failed");
                await _vnPaymentResponseRepo.CreateVNPaymentResponse(VNPaymentResponseModel);
                await _orderService.RevertOrder(createVNPaymentResponseDTO.OrderId);
                return Ok("Payment processing failed miserably");
            }
        }
    }
}
