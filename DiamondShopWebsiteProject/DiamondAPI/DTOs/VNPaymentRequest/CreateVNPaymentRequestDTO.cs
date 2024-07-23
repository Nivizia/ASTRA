namespace DiamondAPI.DTOs.VNPaymentRequest
{
    public class CreateVNPaymentRequestDTO
    {
        public Guid OrderId { get; set; }
        public bool IsDeposit { get; set; }
    }
}
