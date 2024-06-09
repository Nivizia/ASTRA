using DiamondAPI.Data;
using DiamondAPI.DTOs.Ring;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace DiamondAPI.Repositories
{
    public class RingRepository : IRingRepository
    {
        private readonly DiamondprojectContext _context;

        public RingRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Ring> CreateAsync(Ring ringModel)
        {
            await _context.Rings.AddAsync(ringModel);
            await _context.SaveChangesAsync();
            return ringModel;
        }

        public async Task<Ring?> DeleteAsync(Guid R_ProductID)
        {
            var ringModel = await _context.Rings.FirstOrDefaultAsync(r => r.RingId == R_ProductID);
            if (ringModel == null)
            {
                return null;
            }
            _context.Rings.Remove(ringModel);
            await _context.SaveChangesAsync();
            return ringModel;
        }

        public async Task<List<Ring>> GetAllAsync()
        {
            return await _context.Rings.ToListAsync();
        }

        public async Task<Ring?> GetByIDAsync(Guid R_ProductID)
        {
            return await _context.Rings.FindAsync(R_ProductID);
        }

        public async Task<Ring?> UpdateAsync(Guid R_ProductID, UpdateRingRequestDTO ringDTO)
        {
            var existingRing = await _context.Rings.FirstOrDefaultAsync(r => r.RingId == R_ProductID);

            if (existingRing == null)
            {
                return null;
            }

            existingRing.Name = ringDTO.Name;
            existingRing.Price = ringDTO.Price;
            existingRing.StockQuantity = ringDTO.StockQuantity;
            existingRing.ImageUrl = ringDTO.ImageUrl;
            existingRing.MetalType = ringDTO.MetalType;
            existingRing.RingSize = ringDTO.RingSize;

            await _context.SaveChangesAsync();

            return existingRing;
        }
    }
}
