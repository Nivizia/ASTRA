using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class DiamondRepository : IDiamondRepository
    {
        private readonly DiamondprojectContext _context;
        public DiamondRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Diamond> CreateAsync(Diamond diamondModel)
        {
            await _context.Diamonds.AddAsync(diamondModel);
            await _context.SaveChangesAsync();
            return diamondModel;
        }

        public async Task<Diamond?> DeleteAsync(Guid D_ProductID)
        {
            var diamondModel = await _context.Diamonds.FirstOrDefaultAsync(d => d.DProductId == D_ProductID);
            if (diamondModel == null)
            {
                return null;
            }
            _context.Diamonds.Remove(diamondModel);
            await _context.SaveChangesAsync();
            return diamondModel;
        }

        public async Task<List<Diamond>> GetAllAsync()
        {
            return await _context.Diamonds.ToListAsync();
        }

        public async Task<Diamond?> GetByIDAsync(Guid D_ProductID)
        {
            return await _context.Diamonds.FindAsync(D_ProductID);
        }

        public async Task<Diamond?> UpdateAsync(Guid D_ProductID, UpdateDiamondRequestDTO diamondDTO)
        {
            var existingDiamond = await _context.Diamonds.FirstOrDefaultAsync(d => d.DProductId == D_ProductID);

            if (existingDiamond == null)
            {
                return null;
            }

            existingDiamond.Name = diamondDTO.Name;
            existingDiamond.Price = diamondDTO.Price;
            existingDiamond.ImageUrl = diamondDTO.ImageUrl;
            existingDiamond.DType = diamondDTO.DType;
            existingDiamond.CaratWeight = diamondDTO.CaratWeight;
            existingDiamond.Color = diamondDTO.Color;
            existingDiamond.Clarity = diamondDTO.Clarity;
            existingDiamond.Cut = diamondDTO.Cut;

            await _context.SaveChangesAsync();

            return existingDiamond;
        }
    }
}
