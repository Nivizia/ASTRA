﻿namespace DiamondAPI.DTOs.VNPaymentResponse
{
    public class CreateVNPaymentResponseDTO
    {
        public Guid OrderId { get; set; }

        public string? ResponseCode { get; set; }

        public string? TransactionStatus { get; set; }

        public decimal? Amount { get; set; }

        public string? BankCode { get; set; }

        public string? BankTransactionNumber { get; set; }

        public string? CardType { get; set; }

        public string? OrderInfo { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
