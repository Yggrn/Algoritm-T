using DBlayer.Entities;

namespace DBlayer.Interfaces
{
    public interface IOrganizationsRepository : IMainRepository<Organizations.OrganizationsArray>
    {
        Task<Organizations.OrganizationsArray?> GetByApiToken(string name);
    }
}
