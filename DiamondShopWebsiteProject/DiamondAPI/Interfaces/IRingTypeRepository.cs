namespace DiamondAPI.Interfaces
{
    public interface IRingTypeRepository
    {
        public Task<Guid?> GetRingTypeIdFromName(string? ringTypeName);
    }
}
