using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext:DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            :base(options)
        {            
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {         

                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilde)
        {
            modelBuilde.Entity<Product>(entity=> 
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

                entity.Property(e => e.Quantity)               
                .IsRequired();

                entity.Property(e => e.Price)
                .IsRequired();

                //Problem 4
                entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsRequired(false)
                .HasDefaultValue("No description");
                
            });

            modelBuilde.Entity<Store>(entity=> 
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(80);
            });

            modelBuilde.Entity<Customer>(entity => 
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
               .IsUnicode()
               .IsRequired()
               .HasMaxLength(100);

                entity.Property(e => e.Email)
               .IsUnicode(false)
               .IsRequired()
               .HasMaxLength(80);

                entity.Property(e => e.CreditCardNumber)
                .IsRequired(false);
            });

            modelBuilde.Entity<Sale>(entity=> 
            {
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.Date)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

                entity.HasOne(e => e.Product)
                .WithMany(e => e.Sales)
                .HasForeignKey(e => e.ProductId)
                .HasConstraintName("FK_Sales_Product");

                entity.HasOne(e => e.Store)
                .WithMany(e => e.Sales)
                .HasForeignKey(e => e.StoreId)
                .HasConstraintName("FK_Store_Product");

                entity.HasOne(e => e.Customer)
               .WithMany(e => e.Sales)
               .HasForeignKey(e => e.CustomerId)
               .HasConstraintName("FK_Customer_Product");
            });
        }
    }
}
