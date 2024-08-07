﻿using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DiamondAPI.Repositories
{
    public class VNPaymentResponseRepository : IVNPaymentResponseRepository
    {
        private readonly DiamondprojectContext _context;

        public VNPaymentResponseRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<VnpaymentResponse> CreateVNPaymentResponse(VnpaymentResponse vnpaymentResponse)
        {
            await _context.VnpaymentResponses.AddAsync(vnpaymentResponse);
            await _context.SaveChangesAsync();
            return vnpaymentResponse;
        }

        public async Task<bool> PaymentResponseExists(Guid paymentId)
        {
            return await _context.VnpaymentResponses.AnyAsync(r => r.PaymentId == paymentId);
        }
    }
}
