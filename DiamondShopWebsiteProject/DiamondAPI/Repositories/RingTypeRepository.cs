using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class RingTypeRepository : IRingTypeRepository
    {
        private readonly DiamondprojectContext _context;

        public RingTypeRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetRingTypeIdFromName(string? ringTypeName)
        {
            var ringType = await _context.Ringtypes.FirstOrDefaultAsync(rt => rt.TypeName == ringTypeName);

            if (ringType == null)
            {
                return null;
            }

            return ringType.RingTypeId;
        }
    }
}
