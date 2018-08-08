using FastFood.Models;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace FastFood.Data
{
	public class FastFoodDbContext : DbContext
	{
		public FastFoodDbContext()
		{
		}

		public FastFoodDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<Position> Positions { get; set; }

        public DbSet<Employee> Employees{ get; set; }

        public DbSet<Order> Orders{ get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Item> Items{ get; set; }

        public DbSet<Category> Categories{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (!builder.IsConfigured)
			{
				builder.UseLazyLoadingProxies().UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<OrderItem>(ent=> 
            {
                ent.HasKey(x => new { x.ItemId, x.OrderId });
            });

            builder.Entity<Position>(ent=>
            {
                ent.HasIndex(x => x.Name)
                .IsUnique();
            });

            builder.Entity<Item>(ent =>
            {
                ent.HasIndex(x => x.Name)
                .IsUnique();
            });

            //builder.Entity<Order>(ent=>
            //{
            //    ent.Property(t => t.Type)
            //    .HasConversion(
            //        t => t.ToString(),
            //        t => (OrderType)Enum.Parse(typeof(OrderItem), t, true))
            //    .HasMaxLength(7);
                    
            //});
        }
	}
}