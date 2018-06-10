using Cars.Data.Models;
using Cars.Data.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data
{
    public class CarsDbContext:DbContext
    {
        public CarsDbContext()
        {
        }

        public CarsDbContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Car> Cars{ get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<LicensePlate> LicensePlates { get; set; }

        public DbSet<Make> Makes { get; set; }      
        
        public DbSet<Dealership> Dealerships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CarConfiguration());

            builder.ApplyConfiguration(new EngineConfiguration());


            builder.ApplyConfiguration(new CarDealershipConfiguration());

            builder.ApplyConfiguration(new MakeConfiguration());

            builder.ApplyConfiguration(new DealershipConfiguration());
        }
    }
}
