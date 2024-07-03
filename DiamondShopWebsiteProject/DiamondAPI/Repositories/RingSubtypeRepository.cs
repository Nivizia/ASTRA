using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class RingSubtypeRepository : IRingSubtypeRepository
    {
        private readonly DiamondprojectContext _context;

        public RingSubtypeRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetRingSubtypeIdFromName(string? ringSubtypeName)
        {
            var ringSubtype = await _context.Ringsubtypes.FirstOrDefaultAsync(rs => rs.SubtypeName == ringSubtypeName);

            if (ringSubtype == null)
            {
                return null;
            }

            return ringSubtype.RingSubtypeId;
        }
    }
}
