using DiamondAPI.Data;
using DiamondAPI.DTOs.Earring;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class EarringRepository : IEarringRepository
    {
        private readonly DiamondprojectContext _context;
        public EarringRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Earring> CreateAsync(Earring earringModel)
        {
            await _context.Earrings.AddAsync(earringModel);
            await _context.SaveChangesAsync();
            return earringModel;
        }

        public async Task<Earring?> DeleteAsync(Guid EarringID)
        {
            var earringModel = await _context.Earrings.FirstOrDefaultAsync(e => e.EarringId == EarringID);
            if (earringModel == null)
            {
                return null;
            }
            _context.Earrings.Remove(earringModel);
            await _context.SaveChangesAsync();
            return earringModel;
        }

        public async Task<List<Earring>> GetAllAsync()
        {
            return await _context.Earrings.ToListAsync();
        }

        public async Task<Earring?> GetByIDAsync(Guid EarringID)
        {
            return await _context.Earrings.FindAsync(EarringID);
        }

        public async Task<Earring?> UpdateAsync(Guid EarringID, UpdateEarringRequestDTO earringDTO)
        {
            var existingEarring = await _context.Earrings.FirstOrDefaultAsync(e => e.EarringId == EarringID);

            if (existingEarring == null)
            {
                return null;
            }

            existingEarring.Name = earringDTO.Name;
            existingEarring.Price = earringDTO.Price;
            existingEarring.StockQuantity = earringDTO.StockQuantity;
            existingEarring.ImageUrl = earringDTO.ImageUrl;
            existingEarring.MetalType = earringDTO.MetalType;

            await _context.SaveChangesAsync();

            return existingEarring;
        }
    }
}
