using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using BusTicketsSystem.Data.Config;
using System.Linq;

namespace BusTicketsSystem.Data
{   

    public class BusTicketsSystemContext:DbContext
    {
        public BusTicketsSystemContext()
        {            
        }

        public BusTicketsSystemContext(DbContextOptions options)
            :base(options)
        {            
        }
        public DbSet<BusCompany> BusCompanies{ get; set; }
        public DbSet<Ticket> Tickets{ get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Trip> Trips{ get; set; }
        public DbSet<BusStation> BusStations{ get; set; }
        public DbSet<Town> Towns{ get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<BankAccount> BankAccounts{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder
                    .UseLazyLoadingProxies(true)
                    .EnableSensitiveDataLogging(true)
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankAccountConfig());

            modelBuilder.ApplyConfiguration(new BusCompanyConfig());

            modelBuilder.ApplyConfiguration(new BusStationConfig());

            modelBuilder.ApplyConfiguration(new CustomerConfig());

            modelBuilder.ApplyConfiguration(new ReviewConfig());

            modelBuilder.ApplyConfiguration(new TicketConfig());

            modelBuilder.ApplyConfiguration(new TownConfig());

            modelBuilder.ApplyConfiguration(new BusCompanyConfig());
            
        }
    }
}
