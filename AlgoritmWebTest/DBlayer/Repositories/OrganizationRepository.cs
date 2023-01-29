using DBlayer.Entities;
using DBlayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBlayer.Repositories
{
    public class OrganizationRepository : IOrganizationsRepository
    {
        ApplicationDbContext db;
        public OrganizationRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> CreateAsync(Organizations.OrganizationsArray entity)
        {
            await db.AddAsync(entity);
            db.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteAsync(Organizations.OrganizationsArray entity)
        {
            db.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<Organizations.OrganizationsArray?> Get(int OrgId)
        {
            return await db.organizations.FirstOrDefaultAsync(x => x.id == OrgId.ToString());
        }
        public async Task<Organizations.OrganizationsArray?> GetByApiToken(string name)
        {
            return await db.organizations.FirstOrDefaultAsync(x => x.apiToken == name);
        }
        public async Task<List<Organizations.OrganizationsArray>> SelectAsync()
        {
            return await db.organizations.ToListAsync();
        }
    }
}
