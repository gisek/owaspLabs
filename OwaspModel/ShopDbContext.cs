using Microsoft.EntityFrameworkCore;

namespace OwaspModel
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}