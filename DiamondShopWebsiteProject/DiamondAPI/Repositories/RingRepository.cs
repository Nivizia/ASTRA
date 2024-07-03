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
            return await _context.Rings
                .Include(r => r.FrameType)
                .Include(r => r.MetalType)
                .Include(r => r.RingSubtype)
                .Include(r => r.RingType)
                .Include(r => r.StoneCut)
                .Include(r => r.SpecialFeature)
                .ToListAsync();
        }

        public async Task<Ring?> GetByIDAsync(Guid? R_ProductID)
        {
            if (R_ProductID == null)
                return null;
            return await _context.Rings
                .Include(r => r.FrameType)
                .Include(r => r.MetalType)
                .Include(r => r.RingSubtype)
                .Include(r => r.RingType)
                .Include(r => r.StoneCut)
                .Include(r => r.SpecialFeature)
                .FirstOrDefaultAsync(r => r.RingId == R_ProductID);
        }

        public async Task<Ring?> UpdateAsync(Guid R_ProductID, UpdateRingRequestDTO updateRingDTO)
        {
            var existingRing = await _context.Rings.FirstOrDefaultAsync(r => r.RingId == R_ProductID);

            if (existingRing == null)
            {
                return null;
            }

            if (existingRing.RingType != null) existingRing.RingType.TypeName = updateRingDTO.RingType ?? existingRing.RingType.TypeName;
            if (existingRing.RingSubtype != null) existingRing.RingSubtype.SubtypeName = updateRingDTO.RingSubtype ?? existingRing.RingSubtype.SubtypeName;
            if (existingRing.FrameType != null) existingRing.FrameType.FrameTypeName = updateRingDTO.FrameType ?? existingRing.FrameType.FrameTypeName;
            if (existingRing.MetalType != null) existingRing.MetalType.MetalTypeName = updateRingDTO.MetalType ?? existingRing.MetalType.MetalTypeName;
            existingRing.RingSize = updateRingDTO.RingSize;
            existingRing.RingName = updateRingDTO.RingName;
            existingRing.Price = updateRingDTO.Price;
            existingRing.StockQuantity = updateRingDTO.StockQuantity;
            existingRing.ImageUrl = updateRingDTO.ImageUrl;

            await _context.SaveChangesAsync();

            return existingRing;
        }
    }
}
