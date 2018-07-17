using Microsoft.EntityFrameworkCore;
using ProductShop.Data.Configurations;
using ProductShop.Models;

namespace ProductShop.Data
{
    public class ProductShopDbContext : DbContext
    {
        public ProductShopDbContext()
        {
        }
        public ProductShopDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users{ get; set; }

        public DbSet<Product> Products{ get; set; }

        public DbSet<Category> Categories{ get; set; }

        public DbSet<CategoryProduct> CategoryProducts{ get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(true)
                    .UseSqlServer(Configuration.ConfigurationString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
        }
    }
}
