using Microsoft.EntityFrameworkCore;
using System;
using XMLProductShop.Models;

namespace XMLProductShop.Data
{
    public class XmlProductShopContext : DbContext
    {
        public XmlProductShopContext()
        {
        }
        public XmlProductShopContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategorieProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>()
                .HasKey(x => new { x.CategoryId, x.ProductId });

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Buyer)
                .WithMany(x => x.ProductsToBuy)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Seller)
                .WithMany(x => x.ProductsToSell)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
