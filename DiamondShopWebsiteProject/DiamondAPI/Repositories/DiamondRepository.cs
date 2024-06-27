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

        public async Task<List<Diamond>> FilterAsync(String DType, decimal? LowerPrice, decimal? UpperPrice, double? LowerCaratWeight, double? UpperCaratWeight, int? LowerColor, int? UpperColor, int? LowerClarity, int? UpperClariry, int? LowerCut, int? UpperCut)
        {
            var diamonds = _context.Diamonds.AsQueryable();

            if (!string.IsNullOrEmpty(DType))
            {
                diamonds = diamonds.Where(d => d.DType == DType);
            }

            if (LowerPrice != 0)
            {
                diamonds = diamonds.Where(d => d.Price >= LowerPrice);
            }

            if (UpperPrice != 0)
            {
                diamonds = diamonds.Where(d => d.Price <= UpperPrice);
            }

            if (LowerCaratWeight != 0)
            {
                diamonds = diamonds.Where(d => d.CaratWeight >= LowerCaratWeight);
            }

            if (UpperCaratWeight != 0)
            {
                diamonds = diamonds.Where(d => d.CaratWeight <= UpperCaratWeight);
            }

            if (LowerColor != 0)
            {
                diamonds = diamonds.Where(d => d.Color >= LowerColor);
            }

            if (UpperColor != 0)
            {
                diamonds = diamonds.Where(d => d.Color <= UpperColor);
            }

            if (LowerClarity != 0)
            {
                diamonds = diamonds.Where(d => d.Clarity >= LowerClarity);
            }

            if (UpperClariry != 0)
            {
                diamonds = diamonds.Where(d => d.Clarity <= UpperClariry);
            }

            if (LowerCut != 0)
            {
                diamonds = diamonds.Where(d => d.Cut >= LowerCut);
            }

            if (UpperCut != 0)
            {
                diamonds = diamonds.Where(d => d.Cut <= UpperCut);
            }

            return await diamonds.ToListAsync();
        }

        public async Task<List<Diamond>> GetAllAsync()
        {
            return await _context.Diamonds.ToListAsync();
        }

        public async Task<Diamond?> GetByIDAsync(Guid D_ProductID)
        {
            return await _context.Diamonds.FindAsync(D_ProductID);
        }

        public async Task<Diamond?> UpdateAsync(Guid D_ProductID, ModelUpdateDiamondRequestDTO diamondDTO)
        {
            var existingDiamond = await _context.Diamonds.FirstOrDefaultAsync(d => d.DProductId == D_ProductID);

            if (existingDiamond == null)
            {
                return null;
            }

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
