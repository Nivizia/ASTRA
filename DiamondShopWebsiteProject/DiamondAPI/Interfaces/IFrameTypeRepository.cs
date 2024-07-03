namespace DiamondAPI.Interfaces
{
    public interface IFrameTypeRepository
    {
        public Task<Guid?> GetFrameTypeIdFromName(string? frameTypeName);
    }
}
