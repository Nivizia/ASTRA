using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DiamondAPI.Repositories
{
    public class VNPaymentRepository : IVNPaymentRequestRepository
    {
        private readonly DiamondprojectContext _context;

        public VNPaymentRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<VnpaymentRequest> CreateVNPaymentRequest(VnpaymentRequest vnpaymentRequest)
        {
            await _context.VnpaymentRequests.AddAsync(vnpaymentRequest);
            await _context.SaveChangesAsync();
            return vnpaymentRequest;
        }
    }
}
