using Microsoft.EntityFrameworkCore;

namespace AlgoritmWeb.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Organizations.OrganizationsArray> organizations { get; set; } = null!;
        public DbSet<ItemsArray> terminalGroups { get; set; } = null!;
        public DbSet<Menus.Group> menusGroups { get; set; } = null!;
        public DbSet<Menus.Product> menusProducts { get; set; } = null!;
        public DbSet<Menus.Price> menusPrices { get; set; } = null!;
        public DbSet<Menus.SizePrice> menusSizePrices { get; set; } = null!;
        public DbSet<Menus.OrderType> OrderTypes { get; set; }= null!;
        public DbSet<Menus.ItemOrder> ItemOrders { get; set; }= null!;
        public DbSet<Discounts.Item> discountItems { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }

    }

 
}
