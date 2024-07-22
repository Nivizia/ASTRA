using DiamondAPI.Models;
using DiamondAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("api/vnpay")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly VNPayService _vnPayService;

        public VNPayController(VNPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("create-payment-url")]
        public IActionResult CreatePaymentUrl([FromBody] VnpaymentRequest model)
        {
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
