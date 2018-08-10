namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            :base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAid> AnimalAids{ get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<Vet> Vets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ProcedureAnimalAid>(x => 
                     x.HasKey(z => new { z.AnimalAidId, z.ProcedureId }));

            builder
                .Entity<AnimalAid>(x =>
                    x.HasIndex(z => z.Name)
                    .IsUnique());

            builder
                .Entity<Vet>(x =>
                    x.HasIndex(z => z.Name)
                    .IsUnique());

            //builder
            //    .Entity<Animal>(ent => ent.HasOne(p => p.PassportSerialNumber)
            //        .WithOne(a => a.Animal)
            //        .HasForeignKey<Passport>(a => a.AnimalId)
            //        .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
