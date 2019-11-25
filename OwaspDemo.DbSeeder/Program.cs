using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OwaspModel;

namespace OwaspDemo.DbSeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=GISEK\\SQLEXPRESS;Database=Owasp;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using(var context = new ShopDbContext(optionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }
    }
}
