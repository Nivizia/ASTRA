using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Repositories
{
    public class ShapeRepository : IShapeRepository
    {
        private readonly DiamondprojectContext _context;

        public ShapeRepository(DiamondprojectContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetIDByName(string shapeName)
        {
            var Shape = await _context.Shapes.FirstOrDefaultAsync(s => s.ShapeName == shapeName);
            if (Shape == null)
            {
                return null;
            }
            return Shape.ShapeId;
        }

        public async Task<string?> GetShapeNameById(Guid shapeId)
        {
            var shape = await _context.Shapes.FindAsync(shapeId);
            if (shape == null)
            {
                return null;
            }
            return shape.ShapeName;
        }
    }
}
