using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class FrameTypeRepository : IFrameTypeRepository
    {
        private readonly DiamondprojectContext _context;

        public FrameTypeRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetFrameTypeIdFromName(string? frameTypeName)
        {
            var frameType = await _context.Frametypes.FirstOrDefaultAsync(ft => ft.FrameTypeName == frameTypeName);

            if (frameType == null)
            {
                return null;
            }

            return frameType.FrameTypeId;
        }
    }
}
