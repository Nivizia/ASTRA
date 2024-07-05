using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
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

        public async Task<Diamond?> CreateAsync(Diamond diamond)
        {
            await _context.Diamonds.AddAsync(diamond);
            await _context.SaveChangesAsync();
            return diamond;

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

        public async Task<List<Diamond>> FilterAsync(String ShapeName, decimal? LowerPrice, decimal? UpperPrice, double? LowerCaratWeight, double? UpperCaratWeight, int? LowerColor, int? UpperColor, int? LowerClarity, int? UpperClariry, int? LowerCut, int? UpperCut)
        {
            var diamonds = _context.Diamonds.AsQueryable();

            if (!string.IsNullOrEmpty(ShapeName))
            {
                diamonds = diamonds.Where(d => d.Shape != null && d.Shape.ShapeName == ShapeName);
            }

            if (LowerPrice != 0)
            {
                
            }

            if (UpperPrice != 0)
            {
                
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
            return await _context.Diamonds.Include(d => d.Shape).ToListAsync();
        }

        public async Task<Diamond?> GetByIDAsync(Guid? D_ProductID)
        {
            if (D_ProductID == null)
                return null;
            return await _context.Diamonds.Include(d => d.Shape).FirstOrDefaultAsync(d => d.DProductId == D_ProductID);
        }

        public async Task<bool> IsAvailable(Guid? D_ProductID)
        {
            if (D_ProductID == null)
            {
                return false;
            }

            var diamond = await _context.Diamonds.FirstOrDefaultAsync(d => d.DProductId == D_ProductID);
            if (diamond == null)
            {
                return false;
            }
            return diamond.Available ?? false;
        }

        public async Task<Diamond?> UpdateAsync(Guid D_ProductID, ModelUpdateDiamondRequestDTO diamondDTO)
        {
            var existingDiamond = await _context.Diamonds.FirstOrDefaultAsync(d => d.DProductId == D_ProductID);

            if (existingDiamond == null)
            {
                return null;
            }

            existingDiamond.ImageUrl = diamondDTO.ImageUrl;
            if (existingDiamond.Shape != null)
            {
                existingDiamond.Shape.ShapeName = diamondDTO.ShapeName ?? existingDiamond.Shape.ShapeName;
            }
            existingDiamond.CaratWeight = diamondDTO.CaratWeight;
            existingDiamond.Color = diamondDTO.Color;
            existingDiamond.Clarity = diamondDTO.Clarity;
            existingDiamond.Cut = diamondDTO.Cut;

            await _context.SaveChangesAsync();

            return existingDiamond;
        }
    }
}
