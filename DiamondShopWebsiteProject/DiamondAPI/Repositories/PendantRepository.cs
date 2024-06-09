using DiamondAPI.Data;
using DiamondAPI.DTOs.Pendant;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class PendantRepository : IPendantRepository
    {
        private readonly DiamondprojectContext _context;
        public PendantRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Pendant> CreateAsync(Pendant pendantModel)
        {
            await _context.Pendants.AddAsync(pendantModel);
            await _context.SaveChangesAsync();
            return pendantModel;
        }

        public async Task<Pendant?> DeleteAsync(Guid PendantID)
        {
            var pendantModel = await _context.Pendants.FirstOrDefaultAsync(p => p.PendantId == PendantID);
            if (pendantModel == null)
            {
                return null;
            }

            _context.Pendants.Remove(pendantModel);
            await _context.SaveChangesAsync();

            return pendantModel;
        }

        public async Task<List<Pendant>> GetAllAsync()
        {
            return await _context.Pendants.ToListAsync();
        }

        public async Task<Pendant?> GetByIDAsync(Guid PendantID)
        {
            return await _context.Pendants.FindAsync(PendantID);
        }

        public async Task<Pendant?> UpdateAsync(Guid PendantID, UpdatePendantRequestDTO pendantDTO)
        {
            var existingPendant = await _context.Pendants.FirstOrDefaultAsync(p => p.PendantId == PendantID);
            if (existingPendant == null)
            {
                return null;
            }

            existingPendant.Name = pendantDTO.Name;
            existingPendant.Price = pendantDTO.Price;
            existingPendant.StockQuantity = pendantDTO.StockQuantity;
            existingPendant.ImageUrl = pendantDTO.ImageUrl;
            existingPendant.MetalType = pendantDTO.MetalType;

            await _context.SaveChangesAsync();

            return existingPendant;
        }
    }
}
