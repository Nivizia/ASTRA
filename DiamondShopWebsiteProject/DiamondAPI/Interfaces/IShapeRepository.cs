using Microsoft.Identity.Client;

namespace DiamondAPI.Interfaces
{
    public interface IShapeRepository
    {
        public Task<String?> GetShapeNameById(Guid shapeId);
        public Task<Guid?> GetIDByName(String shapeName);
    }
}
