namespace DiamondAPI.Interfaces
{
    public interface IMetalTypeRepository
    {
        public Task<Guid> GetMetalTypeIdFromName();
    }
}
