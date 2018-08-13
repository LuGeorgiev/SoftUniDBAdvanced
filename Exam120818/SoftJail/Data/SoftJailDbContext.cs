namespace SoftJail.Data
{
	using Microsoft.EntityFrameworkCore;
    using SoftJail.Data.Models;

    public class SoftJailDbContext : DbContext
	{
		public SoftJailDbContext()
		{
		}

		public SoftJailDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<Prisoner> Prisoners { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
                    .UseLazyLoadingProxies()
					.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<OfficerPrisoner>(ent=> 
            {
                ent.HasKey(x => new { x.OfficerId, x.PrisonerId });

                ent.HasOne(p => p.Prisoner)
                .WithMany(o => o.PrisonerOfficers)
                .HasForeignKey(p => p.PrisonerId)
                .OnDelete(DeleteBehavior.Restrict);

                ent.HasOne(p => p.Officer)
                .WithMany(o => o.OfficerPrisoners)
                .HasForeignKey(p => p.OfficerId)
                .OnDelete(DeleteBehavior.Restrict);
            });               

            
		}
	}
}