namespace DBlayer.Interfaces
{
    public interface IMainRepository <TClass>
    {
        Task<bool> CreateAsync(TClass entity);
        Task<TClass?> Get(int OrgId);
        Task<List<TClass>> SelectAsync();
        Task<bool> DeleteAsync(TClass entity);
    }
}
