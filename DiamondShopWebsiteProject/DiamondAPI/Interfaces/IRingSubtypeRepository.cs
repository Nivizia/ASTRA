namespace DiamondAPI.Interfaces
{
    public interface IRingSubtypeRepository
    {
        public Task<Guid?> GetRingSubtypeIdFromName(string? ringSubtypeName);
    }
}
