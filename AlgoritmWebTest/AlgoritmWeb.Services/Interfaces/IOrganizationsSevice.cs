using AlgoritmWeb.DBlayer.Response;
using DBlayer.Entities;

namespace AlgoritmWeb.Services.Interfaces
{
    public interface IOrganizationsSevice
    {
        Task<IBaseResponse<IEnumerable<Organizations.OrganizationsArray>>> GetOrganization();
        Task<IBaseResponse<Organizations.OrganizationsArray>> InsertOrganization(string apitoken);
    }
}
