using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class MetalTypeRepository : IMetalTypeRepository
    {
        private readonly DiamondprojectContext _context;

        public MetalTypeRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetMetalTypeIdFromName(string? metalTypeName)
        {
            var metalType = await _context.Metaltypes.FirstOrDefaultAsync(mt => mt.MetalTypeName == metalTypeName);

            if (metalType == null)
            {
                return null;
            }

            return metalType.MetalTypeId;
        }
    }
}
