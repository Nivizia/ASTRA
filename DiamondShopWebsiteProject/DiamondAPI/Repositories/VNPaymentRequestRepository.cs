using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class VNPaymentRequestRepository : IVNPaymentRequestRepository
    {
        private readonly DiamondprojectContext _context;

        public VNPaymentRequestRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<VnpaymentRequest> CreateVNPaymentRequest(VnpaymentRequest vnpaymentRequest)
        {
            await _context.VnpaymentRequests.AddAsync(vnpaymentRequest);
            await _context.SaveChangesAsync();
            return vnpaymentRequest;
        }

        public async Task<Guid> GetOrderRequest(Guid requestId)
        {
            var vnpayrequest = await _context.VnpaymentRequests.FirstOrDefaultAsync(v => v.RequestId == requestId);
            if (vnpayrequest == null)
            {
                return Guid.Empty;
            }
            return vnpayrequest.OrderId;
        }
    }
}
