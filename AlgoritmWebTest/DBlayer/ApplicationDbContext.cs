using Microsoft.EntityFrameworkCore;
using DBlayer.Entities;
namespace DBlayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Organizations.OrganizationsArray> organizations { get; set; } = null!;
        public DbSet<ItemsArray> terminalGroups { get; set; } = null!;
        public DbSet<Menus.Group> menusGroups { get; set; } = null!;
        public DbSet<Menus.Product> menusProducts { get; set; } = null!;
        public DbSet<Menus.Price> menusPrices { get; set; } = null!;
        public DbSet<Menus.SizePrice> menusSizePrices { get; set; } = null!;
        public DbSet<Menus.OrderType> OrderTypes { get; set; } = null!;
        public DbSet<Menus.ItemOrder> ItemOrders { get; set; } = null!;
        public DbSet<Discounts.Item> discountItems { get; set; } = null!;
        public DbSet<Tables.Table> tables { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}